using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Configs;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Utils.Disposable;
using MVC.Views;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class FightSceneController : DisposableWithCts, IAsyncStartable, IDisposable
    {
        private readonly FightSceneFactory _factory;
        private readonly FightSceneStorage _storage;
        private readonly PlayerLifetimeScopeFactory _playerLifetimeScopeFactory;

        private readonly FightSceneModel _fightSceneModel;
        private readonly PlayerInputConfig[] _inputConfigs;

        public FightSceneController(FightSceneFactory factory, FightSceneStorage storage,
            PlayerLifetimeScopeFactory playerLifetimeScopeFactory, FightSceneModel fightSceneModel,
            PlayerInputConfig[] inputConfigs)
        {
            _factory = factory;
            _storage = storage;
            _playerLifetimeScopeFactory = playerLifetimeScopeFactory;
            _fightSceneModel = fightSceneModel;
            _inputConfigs = inputConfigs;
        }

        async UniTask IAsyncStartable.StartAsync(CancellationToken token)
        {
            _factory.CreateFightLocation();
            _factory.CreatePlayers();

            int playerIndex = 0;

            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var playerLifetimeScope =
                    _playerLifetimeScopeFactory.CreatePlayerLifetimeScope(playerContainer, _inputConfigs[playerIndex]);

                _fightSceneModel.PlayerLifetimeScopes.Add(playerLifetimeScope);
                playerIndex++;
            }

            await UniTask.DelayFrame(1, cancellationToken: token);

            InitializePlayers();
        }

        void IDisposable.Dispose()
        {
            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var playerModel = playerContainer.PlayerModel;
                var opponentModel = _storage.OpponentContainerByPlayer[playerContainer].PlayerModel;

                playerModel.OnPlayerAttacked -= OnPlayerAttacked;
                playerModel.OnLose -= opponentModel.ScoreWin;
            }

            _fightSceneModel.OnPlayerSideCheck -= SetPlayerFaceOpponent;
        }

        private void InitializePlayers()
        {
            SetOpponentsForPlayers();

            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var playerModel = playerContainer.PlayerModel;
                var opponentModel = _storage.OpponentContainerByPlayer[playerContainer].PlayerModel;

                SetPlayerFaceOpponent(playerModel);

                playerModel.OnPlayerAttacked += OnPlayerAttacked;
                playerModel.OnLose += opponentModel.ScoreWin;
            }

            _fightSceneModel.OnPlayerSideCheck += SetPlayerFaceOpponent;
        }

        private void SetOpponentsForPlayers()
        {
            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var opponentContainer = _storage.PlayerContainers.First(c => c != playerContainer);

                _storage.OpponentContainerByPlayer.Add(playerContainer, opponentContainer);
            }
        }

        private void SetPlayerFaceOpponent(PlayerModel playerModel)
        {
            var playerContainer = _storage.PlayerContainers.First(p => p.PlayerModel == playerModel);

            var playerTransform = playerContainer.PlayerView.transform;

            var opponentTransform = _storage.OpponentContainerByPlayer[playerContainer].PlayerView.transform;

            if (playerModel.AtLeftSide && playerTransform.position.x > opponentTransform.position.x)
            {
                playerModel.TurnPlayer();
                playerModel.AtLeftSide = false;

                var localScale = playerTransform.localScale;
                localScale.z = -1;
                playerTransform.localScale = localScale;
            }

            if (!playerModel.AtLeftSide && playerTransform.position.x < opponentTransform.position.x)
            {
                playerModel.TurnPlayer();
                playerModel.AtLeftSide = true;

                var localScale = playerTransform.localScale;
                localScale.z = 1;
                playerTransform.localScale = localScale;
            }
        }

        private void OnPlayerAttacked(PlayerModel playerModel, TriggerDetectorView attackHitBoxView)
        {
            playerModel.TakeDamage(_storage.AttackModelsByView[attackHitBoxView].Damage);
        }
    }
}