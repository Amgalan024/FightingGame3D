using System;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC.StateMachine.States;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using MVC_Pattern.Scripts.SettingsMenu.Configs;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerMainController : IInitializable, IStartable, IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly PlayerContainer _opponentContainer;
        private readonly IStateMachine _stateMachine;
        private readonly FightSceneModel _fightSceneModel;
        private readonly Transform _parent;
        private FightSettings _fightSettings;

        public PlayerMainController(PlayerContainer playerContainer, IStateMachine stateMachine,
            LifetimeScope lifetimeScope, FightSceneModel fightSceneModel, FightSettings fightSettings)
        {
            _stateMachine = stateMachine;
            _fightSceneModel = fightSceneModel;
            _fightSettings = fightSettings;
            _parent = lifetimeScope.transform;
            _playerModel = playerContainer.Model;
            _playerView = playerContainer.View;
            _opponentContainer = playerContainer.OpponentContainer;
        }

        void IInitializable.Initialize()
        {
            _playerModel.OnLose += HandleLose;
            _playerModel.OnWin += HandleWin;
        }

        void IStartable.Start()
        {
            _playerView.SetParent(_parent);
        }

        void IDisposable.Dispose()
        {
            _playerModel.OnLose -= HandleLose;
            _playerModel.OnWin -= HandleWin;
        }

        private void HandleLose()
        {
            _opponentContainer.Model.ScoreWin();
            _stateMachine.ChangeState<LoseState>();
        }

        private void HandleWin()
        {
            if (_playerModel.RoundScore >= _fightSettings.MaxRounds)
            {
                _fightSceneModel.InvokeFightEnd();
            }
            else
            {
                _fightSceneModel.InvokeRoundEnd();
            }

            _stateMachine.ChangeState<WinState>();
        }
    }
}