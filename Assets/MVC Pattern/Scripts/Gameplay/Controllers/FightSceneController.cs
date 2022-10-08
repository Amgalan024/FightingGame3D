using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Configs;
using MVC.Gameplay.Models;
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

            foreach (var modelView in _storage.PlayerModelsByView)
            {
                var characterConfig = _storage.CharacterConfigsByModel[modelView.Value];

                var playerLifetimeScope =
                    _playerLifetimeScopeFactory.CreatePlayerLifetimeScope(modelView.Value, modelView.Key,
                        _inputConfigs[playerIndex], characterConfig);

                _fightSceneModel.PlayerLifetimeScopes.Add(playerLifetimeScope);
                playerIndex++;
            }

            await UniTask.DelayFrame(1, cancellationToken: token);

            _storage.PlayerModels.ForEach(SetPlayerFaceToFace);

            _fightSceneModel.OnPlayerSideCheck += SetPlayerFaceToFace;

            _storage.PlayerModels[0].OnPlayerAttacked += OnPlayerAttacked;
            _storage.PlayerModels[1].OnPlayerAttacked += OnPlayerAttacked;

            _storage.PlayerModels[0].OnLose += _storage.PlayerModels[1].ScoreWin;
            _storage.PlayerModels[1].OnLose += _storage.PlayerModels[0].ScoreWin;
        }

        void IDisposable.Dispose()
        {
            _fightSceneModel.OnPlayerSideCheck -= SetPlayerFaceToFace;

            _storage.PlayerModels[0].OnPlayerAttacked -= OnPlayerAttacked;
            _storage.PlayerModels[1].OnPlayerAttacked -= OnPlayerAttacked;

            _storage.PlayerModels[0].OnLose -= _storage.PlayerModels[1].ScoreWin;
            _storage.PlayerModels[1].OnLose -= _storage.PlayerModels[0].ScoreWin;
        }

        private void SetPlayerFaceToFace(PlayerModel playerModel)
        {
            var playerTransform = _storage.PlayerViewsByModel[playerModel].transform;

            var opponentModel = _storage.PlayerModels.First(p => !p.Equals(playerModel));

            var opponentTransform = _storage.PlayerViewsByModel[opponentModel].transform;
            SetPlayerFaceOpponent(playerModel, playerTransform, opponentTransform);
        }

        private void SetPlayerFaceOpponent(PlayerModel playerModel, Transform playerTransform,
            Transform opponentTransform)
        {
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

        private void OnPlayerAttacked(PlayerModel playerModel, PlayerAttackHitBoxView attackHitBoxView)
        {
            playerModel.TakeDamage(_storage.PlayerAttackModelsByView[attackHitBoxView].Damage);
        }
    }
}