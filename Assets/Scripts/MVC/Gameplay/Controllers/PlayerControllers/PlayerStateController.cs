using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Models;
using MVC.Models;
using MVC.StateMachine;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerStateController : IInitializable, IDisposable, IFixedTickable
    {
        private readonly PlayerStateMachine _stateMachine;
        private readonly StatesContainer _statesContainer;
        private readonly StateMachineModel _stateMachineModel;

        private readonly InputActionModelsContainer _inputActionModelsContainer;

        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;

        private readonly List<IDisposable> _subscriptions = new List<IDisposable>(5);

        public PlayerStateController(PlayerStateMachine stateMachine,
            InputActionModelsContainer inputActionModelsContainer, StatesContainer statesContainer,
            PlayerModel playerModel, PlayerView playerView, StateMachineModel stateMachineModel)
        {
            _stateMachine = stateMachine;
            _statesContainer = statesContainer;
            _playerModel = playerModel;
            _playerView = playerView;
            _stateMachineModel = stateMachineModel;
            _inputActionModelsContainer = inputActionModelsContainer;
        }

        void IInitializable.Initialize()
        {
            _stateMachine.StartStateMachine(_statesContainer.IdleState);

            InitializeInput();
            InitializePlayer();
        }
        
        void IFixedTickable.FixedTick()
        {
            _stateMachineModel.CurrentState.OnFixedTick();
        }

        void IDisposable.Dispose()
        {
            DisposeInput();
            DisposePlayer();
        }

        private void InitializeInput()
        {
            _inputActionModelsContainer.MoveForwardActionModel.OnInput += OnMoveForwardInput;
            _inputActionModelsContainer.MoveBackwardActionModel.OnInput += OnMoveBackwardInput;
            _inputActionModelsContainer.BlockStoppedActionModel.OnInput += OnBlockStopped;
            _inputActionModelsContainer.PunchActionModel.OnInput += OnPunchInput;
            _inputActionModelsContainer.KickActionModel.OnInput += OnKickInput;
            _inputActionModelsContainer.JumpActionModel.OnInput += OnJumpInput;
            _inputActionModelsContainer.CrouchActionModel.OnInput += OnCrouchInput;
            _inputActionModelsContainer.BlockActionModel.OnInput += OnBlockInput;
        }

        private void DisposeInput()
        {
            _inputActionModelsContainer.MoveForwardActionModel.OnInput -= OnMoveForwardInput;
            _inputActionModelsContainer.MoveBackwardActionModel.OnInput -= OnMoveBackwardInput;
            _inputActionModelsContainer.BlockStoppedActionModel.OnInput -= OnBlockStopped;
            _inputActionModelsContainer.PunchActionModel.OnInput -= OnPunchInput;
            _inputActionModelsContainer.KickActionModel.OnInput -= OnKickInput;
            _inputActionModelsContainer.JumpActionModel.OnInput -= OnJumpInput;
            _inputActionModelsContainer.CrouchActionModel.OnInput -= OnCrouchInput;
            _inputActionModelsContainer.BlockActionModel.OnInput -= OnBlockInput;
        }

        private void InitializePlayer()
        {
            _playerView.HitBoxView.OnColliderEnter += OnColliderEnter;
            _playerView.HitBoxView.OnColliderExit += OnColliderExit;

            _playerModel.OnWin += OnWin;
            _playerModel.OnLose += OnLose;

            _subscriptions.Add(_playerModel.IsAttacking.Subscribe(isAttacking =>
                _playerView.Animator.SetBool("IsAttacking", isAttacking)));

            _subscriptions.Add(_playerModel.IsGrounded.Subscribe(isGrounded =>
                _playerView.Animator.SetBool("IsGrounded", isGrounded)));

            _subscriptions.Add(_playerModel.IsCrouching.Subscribe(isCrouching =>
                _playerView.Animator.SetBool("IsCrouching", isCrouching)));
        }

        private void DisposePlayer()
        {
            _playerView.HitBoxView.OnColliderEnter -= OnColliderEnter;
            _playerView.HitBoxView.OnColliderExit -= OnColliderExit;

            _playerModel.OnWin -= OnWin;
            _playerModel.OnLose -= OnLose;

            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private void OnColliderEnter(Collider collider)
        {
            _stateMachineModel.CurrentState.OnTriggerEnter(collider);
        }

        private void OnColliderExit(Collider collider)
        {
            _stateMachineModel.CurrentState.OnTriggerExit(collider);
        }

        private void OnWin()
        {
            _stateMachine.ChangeState(_statesContainer.WinState);
        }

        private void OnLose()
        {
            _stateMachine.ChangeState(_statesContainer.LoseState);
        }

        private void OnMoveForwardInput()
        {
            _stateMachine.ChangeState(_statesContainer.MoveForwardState);
        }

        private void OnMoveBackwardInput()
        {
            _stateMachine.ChangeState(_statesContainer.MoveBackwardState);
        }

        private void OnBlockStopped()
        {
            _stateMachine.ChangeState(_statesContainer.PunchState);
        }

        private void OnPunchInput()
        {
            _stateMachine.ChangeState(_statesContainer.PunchState);
        }

        private void OnKickInput()
        {
            _stateMachine.ChangeState(_statesContainer.KickState);
        }

        private void OnJumpInput()
        {
            _stateMachine.ChangeState(_statesContainer.JumpState);
        }

        private void OnCrouchInput()
        {
            _stateMachine.ChangeState(_statesContainer.CrouchState);
        }

        private void OnBlockInput()
        {
            _stateMachine.ChangeState(_statesContainer.BlockState);
        }

       
    }
}