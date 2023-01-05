using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using MVC.Configs.Enums;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.StateMachine.States;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerStatesController : IStartable, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private readonly InputActionModelsContainer _inputActionModelsContainer;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly InputModelsContainer _inputModelsContainer;

        private readonly List<IDisposable> _subscriptions = new List<IDisposable>(5);

        private readonly RunStateModel _runStateModel;

        public PlayerStatesController(PlayerContainer playerContainer, IStateMachine stateMachine,
            RunStateModel runStateModel)
        {
            _playerModel = playerContainer.Model;
            _playerView = playerContainer.View;
            _inputModelsContainer = playerContainer.InputModelsContainer;
            _inputActionModelsContainer = playerContainer.InputActionModelsContainer;
            _stateMachine = stateMachine;
            _runStateModel = runStateModel;
        }

        void IStartable.Start()
        {
            _stateMachine.ChangeState<IdleState>();

            HandleInputEvents();
            HandlePlayerEvents();
        }

        void IDisposable.Dispose()
        {
            DisposeInputEvents();
            DisposePlayerEvents();
        }

        private void HandleInputEvents()
        {
            _inputActionModelsContainer.MoveForwardAction.OnInput += OnMoveForwardInput;
            _inputActionModelsContainer.MoveBackwardAction.OnInput += OnMoveBackwardInput;
            _inputActionModelsContainer.PunchAction.OnInput += _stateMachine.ChangeState<PunchState>;
            _inputActionModelsContainer.KickAction.OnInput += _stateMachine.ChangeState<KickState>;
            _inputActionModelsContainer.JumpAction.OnInput += _stateMachine.ChangeState<JumpState>;
            _inputActionModelsContainer.CrouchAction.OnInput += _stateMachine.ChangeState<CrouchState>;
            _inputActionModelsContainer.StartBlockAction.OnInput += OnStartBlockInput;
            _inputActionModelsContainer.StopBlockAction.OnInput += OnStopBlock;
        }

        private void DisposeInputEvents()
        {
            _inputActionModelsContainer.MoveForwardAction.OnInput -= OnMoveForwardInput;
            _inputActionModelsContainer.MoveBackwardAction.OnInput -= OnMoveBackwardInput;
            _inputActionModelsContainer.PunchAction.OnInput -= _stateMachine.ChangeState<PunchState>;
            _inputActionModelsContainer.KickAction.OnInput -= _stateMachine.ChangeState<KickState>;
            _inputActionModelsContainer.JumpAction.OnInput -= _stateMachine.ChangeState<JumpState>;
            _inputActionModelsContainer.CrouchAction.OnInput -= _stateMachine.ChangeState<CrouchState>;
            _inputActionModelsContainer.StartBlockAction.OnInput -= OnStartBlockInput;
            _inputActionModelsContainer.StopBlockAction.OnInput -= OnStopBlock;
        }

        private void HandlePlayerEvents()
        {
            _playerModel.OnWin += _stateMachine.ChangeState<WinState>;
            _playerModel.OnLose += _stateMachine.ChangeState<LoseState>;

            _subscriptions.Add(_playerModel.IsAttacking.Subscribe(isAttacking =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsAttacking, isAttacking)));

            _subscriptions.Add(_playerModel.IsGrounded.Subscribe(isGrounded =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsGrounded, isGrounded)));

            _subscriptions.Add(_playerModel.IsCrouching.Subscribe(isCrouching =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsCrouching, isCrouching)));
        }

        private void DisposePlayerEvents()
        {
            _playerModel.OnWin -= _stateMachine.ChangeState<WinState>;
            _playerModel.OnLose -= _stateMachine.ChangeState<LoseState>;

            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private void OnMoveForwardInput()
        {
            _runStateModel.SetData(_inputModelsContainer.MoveForward.Key, MovementType.Forward,
                PlayerAnimatorData.Forward);

            _stateMachine.ChangeState<RunState>();
        }

        private void OnMoveBackwardInput()
        {
            _runStateModel.SetData(_inputModelsContainer.MoveBackward.Key, MovementType.Backward,
                PlayerAnimatorData.Backward);

            _stateMachine.ChangeState<RunState>();
        }

        private void OnStartBlockInput()
        {
            _playerModel.IsBlocking.Value = true;
        }

        private void OnStopBlock()
        {
            _playerModel.IsBlocking.Value = false;
        }
    }
}