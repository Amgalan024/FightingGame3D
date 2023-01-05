using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC.Gameplay.Services;
using MVC.Utils.Disposable;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class FightSceneController : DisposableWithCts, IAsyncStartable, IDisposable
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

        async UniTask IAsyncStartable.StartAsync(CancellationToken token)
        {
            _factory.CreateFightLocation();
            _factory.CreatePlayers();

            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var playerLifetimeScope = _playerLifetimeScopeFactory.CreatePlayerLifetimeScope(playerContainer);

                _fightSceneModel.PlayerLifetimeScopes.Add(playerLifetimeScope);
            }

            await UniTask.DelayFrame(1, cancellationToken: token);

            InitializePlayers();
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

                TurnPlayers(playerContainer);

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
            TurnPlayer(playerContainer, SidePlacementType.AtLeftSide, !checkValue);
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