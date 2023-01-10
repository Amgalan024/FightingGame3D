using System;
using MVC.Gameplay.Models.Player;
using MVC.StateMachine.States;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerMainController : IInitializable, IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly PlayerContainer _opponentContainer;
        private readonly IStateMachine _stateMachine;

        private readonly Transform _parent;

        public PlayerMainController(PlayerContainer playerContainer, IStateMachine stateMachine,
            LifetimeScope lifetimeScope)
        {
            _stateMachine = stateMachine;
            _parent = lifetimeScope.transform;
            _playerModel = playerContainer.Model;
            _playerView = playerContainer.View;
            _opponentContainer = playerContainer.OpponentContainer;
        }

        void IInitializable.Initialize()
        {
            _playerView.SetParent(_parent);

            _playerModel.OnLose += HandleLose;
            _playerModel.OnWin += HandleWin;
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
            _stateMachine.ChangeState<WinState>();
        }
    }
}