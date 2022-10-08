using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using MVC.Configs.Enums;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.StateMachineModels;
using MVC.Models;
using MVC.StateMachine.States;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerStateMachineController : IInitializable, IDisposable, IFixedTickable
    {
        private readonly StateMachine.StateMachine _stateMachine;
        private readonly StatesContainer _statesContainer;
        private readonly StateMachineModel _stateMachineModel;
        private readonly StateMachineProxy _stateMachineProxy;
        private readonly InputActionModelsContainer _inputActionModelsContainer;
        private readonly FightSceneModel _fightSceneModel;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly InputModelsContainer _inputModelsContainer;

        private readonly Transform _parent;

        private readonly List<IDisposable> _subscriptions = new List<IDisposable>(5);

        private readonly RunStateModel _runStateModel;

        public PlayerStateMachineController(StateMachine.StateMachine stateMachine,
            InputActionModelsContainer inputActionModelsContainer, StatesContainer statesContainer,
            PlayerModel playerModel, PlayerView playerView, StateMachineModel stateMachineModel,
            StateMachineProxy stateMachineProxy, FightSceneModel fightSceneModel, LifetimeScope lifetimeScope,
            RunStateModel runStateModel, InputModelsContainer inputModelsContainer)
        {
            _stateMachine = stateMachine;
            _statesContainer = statesContainer;
            _playerModel = playerModel;
            _playerView = playerView;
            _stateMachineModel = stateMachineModel;
            _stateMachineProxy = stateMachineProxy;
            _fightSceneModel = fightSceneModel;
            _runStateModel = runStateModel;
            _inputModelsContainer = inputModelsContainer;
            _inputActionModelsContainer = inputActionModelsContainer;
            _parent = lifetimeScope.transform;
        }

        void IInitializable.Initialize()
        {
            _stateMachine.StartStateMachine(_statesContainer.GetStateByType(typeof(IdleState)));

            _stateMachineProxy.OnStateChanged += OnStateChanged;

            InitializeInput();
            InitializePlayer();
        }

        void IFixedTickable.FixedTick()
        {
            _stateMachineModel.CurrentState.OnFixedTick();
        }

        void IDisposable.Dispose()
        {
            _stateMachineProxy.OnStateChanged -= OnStateChanged;

            DisposeInput();
            DisposePlayer();
        }

        private void OnStateChanged(Type stateType)
        {
            _stateMachine.ChangeState(_statesContainer.GetStateByType(stateType));
        }

        private void InitializeInput()
        {
            _inputActionModelsContainer.MoveForwardActionModel.OnInput += OnMoveForwardInput;
            _inputActionModelsContainer.MoveBackwardActionModel.OnInput += OnMoveBackwardInput;
            _inputActionModelsContainer.PunchActionModel.OnInput += OnPunchInput;
            _inputActionModelsContainer.KickActionModel.OnInput += OnKickInput;
            _inputActionModelsContainer.JumpActionModel.OnInput += OnJumpInput;
            _inputActionModelsContainer.CrouchActionModel.OnInput += OnCrouchInput;
            _inputActionModelsContainer.StartBlockActionModel.OnInput += OnStartBlockInput;
            _inputActionModelsContainer.StopBlockActionModel.OnInput += OnStopBlock;
        }

        private void DisposeInput()
        {
            _inputActionModelsContainer.MoveForwardActionModel.OnInput -= OnMoveForwardInput;
            _inputActionModelsContainer.MoveBackwardActionModel.OnInput -= OnMoveBackwardInput;
            _inputActionModelsContainer.PunchActionModel.OnInput -= OnPunchInput;
            _inputActionModelsContainer.KickActionModel.OnInput -= OnKickInput;
            _inputActionModelsContainer.JumpActionModel.OnInput -= OnJumpInput;
            _inputActionModelsContainer.CrouchActionModel.OnInput -= OnCrouchInput;
            _inputActionModelsContainer.StartBlockActionModel.OnInput -= OnStartBlockInput;
            _inputActionModelsContainer.StopBlockActionModel.OnInput -= OnStopBlock;
        }

        private void InitializePlayer()
        {
            _playerView.SetParent(_parent);

            _playerView.OnAttackAnimationEnded += OnAttackAnimationEnded;

            _playerView.TriggerDetector.OnColliderEnter += OnColliderEnter;
            _playerView.TriggerDetector.OnColliderExit += OnColliderExit;

            _playerView.CollisionDetector.OnCollisionEntered += OnCollisionEntered;
            _playerView.CollisionDetector.OnCollisionExited += OnCollisionExit;

            _playerView.SideDetectorView.OnTriggerEntered += OnSideDetectorTriggerEntered;

            _playerModel.OnWin += OnWin;
            _playerModel.OnLose += OnLose;

            _subscriptions.Add(_playerModel.IsAttacking.Subscribe(isAttacking =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsAttacking, isAttacking)));

            _subscriptions.Add(_playerModel.IsGrounded.Subscribe(isGrounded =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsGrounded, isGrounded)));

            _subscriptions.Add(_playerModel.IsCrouching.Subscribe(isCrouching =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsCrouching, isCrouching)));
        }

        private void OnSideDetectorTriggerEntered(Collider collider)
        {
            if (collider.GetComponent<CollisionDetectorView>())
            {
                _fightSceneModel.InvokePlayerSideCheck(_playerModel);
            }
        }

        private void DisposePlayer()
        {
            _playerView.OnAttackAnimationEnded -= OnAttackAnimationEnded;

            _playerView.TriggerDetector.OnColliderEnter -= OnColliderEnter;
            _playerView.TriggerDetector.OnColliderExit -= OnColliderExit;

            _playerView.CollisionDetector.OnCollisionEntered -= OnCollisionEntered;
            _playerView.CollisionDetector.OnCollisionExited -= OnCollisionExit;

            _playerModel.OnWin -= OnWin;
            _playerModel.OnLose -= OnLose;

            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private void OnAttackAnimationEnded()
        {
            _playerModel.IsAttacking.Value = false;
        }

        private void OnColliderEnter(Collider collider)
        {
            _stateMachineModel.CurrentState.OnTriggerEnter(collider);
        }

        private void OnColliderExit(Collider collider)
        {
            _stateMachineModel.CurrentState.OnTriggerExit(collider);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlatformView>())
            {
                _playerModel.IsGrounded.Value = false;
            }
        }

        private void OnCollisionEntered(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlatformView>())
            {
                _playerModel.IsGrounded.Value = true;
            }
        }

        private void OnWin()
        {
            _stateMachine.ChangeState(_statesContainer.GetStateByType(typeof(WinState)));
        }

        private void OnLose()
        {
            _stateMachine.ChangeState(_statesContainer.GetStateByType(typeof(LoseState)));
        }

        private void OnMoveForwardInput()
        {
            _runStateModel.InputKey = _inputModelsContainer.MoveForward.Key;
            _runStateModel.AnimationHash = PlayerAnimatorData.Forward;
            _runStateModel.DirectionType = DirectionType.Forward;
            _runStateModel.DashStateType = typeof(DashForwardState);
            _stateMachine.ChangeState(_statesContainer.GetStateByType(typeof(RunState)));
        }

        private void OnMoveBackwardInput()
        {
            _runStateModel.InputKey = _inputModelsContainer.MoveBackward.Key;
            _runStateModel.AnimationHash = PlayerAnimatorData.Backward;
            _runStateModel.DirectionType = DirectionType.Backward;
            _runStateModel.DashStateType = typeof(DashBackwardState);

            _stateMachine.ChangeState(_statesContainer.GetStateByType(typeof(RunState)));
        }

        private void OnPunchInput()
        {
            _stateMachine.ChangeState(_statesContainer.GetStateByType(typeof(PunchState)));
        }

        private void OnKickInput()
        {
            _stateMachine.ChangeState(_statesContainer.GetStateByType(typeof(KickState)));
        }

        private void OnJumpInput()
        {
            _stateMachine.ChangeState(_statesContainer.GetStateByType(typeof(JumpState)));
        }

        private void OnCrouchInput()
        {
            _stateMachine.ChangeState(_statesContainer.GetStateByType(typeof(CrouchState)));
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