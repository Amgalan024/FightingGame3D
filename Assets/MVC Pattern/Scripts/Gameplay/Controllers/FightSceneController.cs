using System;
using System.Linq;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC.Gameplay.Services;
using MVC.Utils.Disposable;
using MVC.Views;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class FightSceneController : DisposableWithCts, IStartable, IDisposable
    {
        private readonly FightSceneFactory _factory;
        private readonly FightSceneStorage _storage;
        private readonly PlayerLifetimeScopeFactory _playerLifetimeScopeFactory;

        private readonly FightSceneModel _fightSceneModel;

        public FightSceneController(FightSceneFactory factory, FightSceneStorage storage,
            PlayerLifetimeScopeFactory playerLifetimeScopeFactory, FightSceneModel fightSceneModel)
        {
            _factory = factory;
            _storage = storage;
            _playerLifetimeScopeFactory = playerLifetimeScopeFactory;
            _fightSceneModel = fightSceneModel;
        }

        void IStartable.Start()
        {
            _factory.CreateFightLocation();
            _factory.CreatePlayers();

            InitializePlayers();

            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var playerLifetimeScope = _playerLifetimeScopeFactory.CreatePlayerLifetimeScope(playerContainer);

                _fightSceneModel.PlayerLifetimeScopes.Add(playerLifetimeScope);
            }
        }

        void IDisposable.Dispose()
        {
            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var playerModel = playerContainer.Model;
                var opponentModel = playerContainer.OpponentContainer.Model;

                playerModel.OnPlayerAttacked -= OnPlayerAttacked;
                playerModel.OnLose -= opponentModel.ScoreWin;
            }

            _fightSceneModel.OnPlayerSideCheck -= TurnPlayers;
        }

        private void InitializePlayers()
        {
            foreach (var playerContainer in _storage.PlayerContainers)
            {
                SetOpponentForPlayer(playerContainer);

                var playerModel = playerContainer.Model;
                var opponentModel = playerContainer.OpponentContainer.Model;

                playerModel.OnPlayerAttacked += OnPlayerAttacked;
                playerModel.OnLose += opponentModel.ScoreWin;
            }

            _fightSceneModel.OnPlayerSideCheck += TurnPlayers;
        }

        private void SetOpponentForPlayer(PlayerContainer playerContainer)
        {
            var opponentContainer = _storage.PlayerContainers.First(c => c != playerContainer);

            playerContainer.SetOpponent(opponentContainer);
        }

        private void TurnPlayers(PlayerContainer playerContainer)
        {
            var playerTransform = playerContainer.View.transform;

            var opponentTransform = playerContainer.OpponentContainer.View.transform;

            var checkValue = playerTransform.position.x > opponentTransform.position.x;

            TurnPlayer(playerContainer, SidePlacementType.AtLeftSide, checkValue);
            TurnPlayer(playerContainer, SidePlacementType.AtRightSide, !checkValue);
        }

        private void TurnPlayer(PlayerContainer playerContainer, SidePlacementType sidePlacementType, bool checkValue)
        {
            var playerModel = playerContainer.Model;

            if (playerModel.CurrentSidePlacement == sidePlacementType && checkValue)
            {
                playerModel.TurnPlayer();
                playerContainer.View.TurnPlayer((int) playerModel.CurrentSidePlacement);
            }
        }

        private void OnPlayerAttacked(PlayerModel playerModel, TriggerDetectorView attackHitBoxView)
        {
            playerModel.TakeDamage(_storage.AttackModelsByView[attackHitBoxView].Damage);
        }
    }
}