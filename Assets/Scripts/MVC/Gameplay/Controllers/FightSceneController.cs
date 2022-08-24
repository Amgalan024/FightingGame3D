using System;
using MVC.Configs;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class FightSceneController : IStartable, IFixedTickable, IDisposable
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

        void IStartable.Start()
        {
            _factory.CreateFightLocation();
            _factory.CreatePlayers();

            int playerIndex = 0;

            foreach (var modelView in _storage.PlayerModelsByView)
            {
                var comboConfig = _storage.ComboConfigsByModel[modelView.Value];

                var playerLifetimeScope =
                    _playerLifetimeScopeFactory.CreatePlayerLifetimeScope(modelView.Value, modelView.Key,
                        _inputConfigs[playerIndex], comboConfig);

                _fightSceneModel.PlayerLifetimeScopes.Add(playerLifetimeScope);
                playerIndex++;
            }

            _storage.PlayerModels[0].OnLose += _storage.PlayerModels[1].ScoreWin;
            _storage.PlayerModels[1].OnLose += _storage.PlayerModels[0].ScoreWin;
        }

        void IFixedTickable.FixedTick()
        {
            var player1Transform = _storage.PlayerViewsByModel[_storage.PlayerModels[0]].transform;
            var player2Transform = _storage.PlayerViewsByModel[_storage.PlayerModels[1]].transform;

            SetPlayerFaceOpponent(_storage.PlayerModels[0], player1Transform, player2Transform);
            SetPlayerFaceOpponent(_storage.PlayerModels[1], player2Transform, player1Transform);
        }

        void IDisposable.Dispose()
        {
            _storage.PlayerModels[0].OnLose -= _storage.PlayerModels[1].ScoreWin;
            _storage.PlayerModels[1].OnLose -= _storage.PlayerModels[0].ScoreWin;
        }

        private void SetPlayerFaceOpponent(PlayerModel playerModel, Transform playerTransform, Transform enemyTransform)
        {
            if (playerModel.AtLeftSide && playerTransform.position.x > enemyTransform.position.x)
            {
                playerModel.TurnPlayer();
                playerModel.AtRightSide = true;
                playerModel.AtLeftSide = false;

                var localScale = playerTransform.localScale;
                localScale.z = -1;
                playerTransform.localScale = localScale;
            }

            if (playerModel.AtRightSide && playerTransform.position.x < enemyTransform.position.x)
            {
                playerModel.TurnPlayer();
                playerModel.AtLeftSide = true;
                playerModel.AtRightSide = false;

                var localScale = playerTransform.localScale;
                localScale.z = 1;
                playerTransform.localScale = localScale;
            }
        }
    }
}