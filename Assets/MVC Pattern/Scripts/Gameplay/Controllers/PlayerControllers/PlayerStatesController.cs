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
    public class PlayerStatesController : IInitializable, IDisposable
    {
        private readonly StateMachineProxy _stateMachineProxy;
        private readonly InputActionModelsContainer _inputActionModelsContainer;
        private readonly FightSceneModel _fightSceneModel;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly InputModelsContainer _inputModelsContainer;

        private readonly Transform _parent;

        private readonly List<IDisposable> _subscriptions = new List<IDisposable>(5);

        private readonly RunStateModel _runStateModel;

        public PlayerStatesController(InputActionModelsContainer inputActionModelsContainer, PlayerModel playerModel,
            PlayerView playerView, StateMachineProxy stateMachineProxy, FightSceneModel fightSceneModel,
            LifetimeScope lifetimeScope, RunStateModel runStateModel, InputModelsContainer inputModelsContainer)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _stateMachineProxy = stateMachineProxy;
            _fightSceneModel = fightSceneModel;
            _runStateModel = runStateModel;
            _inputModelsContainer = inputModelsContainer;
            _inputActionModelsContainer = inputActionModelsContainer;
            _parent = lifetimeScope.transform;
        }

        void IInitializable.Initialize()
        {
            _stateMachineProxy.ChangeState<IdleState>();

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
            _inputActionModelsContainer.MoveForwardActionModel.OnInput += OnMoveForwardInput;
            _inputActionModelsContainer.MoveBackwardActionModel.OnInput += OnMoveBackwardInput;
            _inputActionModelsContainer.PunchActionModel.OnInput += _stateMachineProxy.ChangeState<PunchState>;
            _inputActionModelsContainer.KickActionModel.OnInput += _stateMachineProxy.ChangeState<KickState>;
            _inputActionModelsContainer.JumpActionModel.OnInput += _stateMachineProxy.ChangeState<JumpState>;
            _inputActionModelsContainer.CrouchActionModel.OnInput += _stateMachineProxy.ChangeState<CrouchState>;
            _inputActionModelsContainer.StartBlockActionModel.OnInput += OnStartBlockInput;
            _inputActionModelsContainer.StopBlockActionModel.OnInput += OnStopBlock;
        }

        private void DisposeInputEvents()
        {
            _inputActionModelsContainer.MoveForwardActionModel.OnInput -= OnMoveForwardInput;
            _inputActionModelsContainer.MoveBackwardActionModel.OnInput -= OnMoveBackwardInput;
            _inputActionModelsContainer.PunchActionModel.OnInput -= _stateMachineProxy.ChangeState<PunchState>;
            _inputActionModelsContainer.KickActionModel.OnInput -= _stateMachineProxy.ChangeState<KickState>;
            _inputActionModelsContainer.JumpActionModel.OnInput -= _stateMachineProxy.ChangeState<JumpState>;
            _inputActionModelsContainer.CrouchActionModel.OnInput -= _stateMachineProxy.ChangeState<CrouchState>;
            _inputActionModelsContainer.StartBlockActionModel.OnInput -= OnStartBlockInput;
            _inputActionModelsContainer.StopBlockActionModel.OnInput -= OnStopBlock;
        }

        private void HandlePlayerEvents()
        {
            _playerView.SetParent(_parent);

            _playerView.OnAttackAnimationEnded += OnAttackAnimationEnded;

            _playerView.CollisionDetector.OnCollisionEntered += OnCollisionEntered;
            _playerView.CollisionDetector.OnCollisionExited += OnCollisionExit;

            _playerView.SideDetectorView.OnTriggerEntered += InvokePlayerSideCheck;
            _playerView.SideDetectorView.OnTriggerExited += InvokePlayerSideCheck;

            _playerModel.OnWin += _stateMachineProxy.ChangeState<WinState>;
            _playerModel.OnLose += _stateMachineProxy.ChangeState<LoseState>;

            _subscriptions.Add(_playerModel.IsAttacking.Subscribe(isAttacking =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsAttacking, isAttacking)));

            _subscriptions.Add(_playerModel.IsGrounded.Subscribe(isGrounded =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsGrounded, isGrounded)));

            _subscriptions.Add(_playerModel.IsCrouching.Subscribe(isCrouching =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsCrouching, isCrouching)));
        }


        private void DisposePlayerEvents()
        {
            _playerView.OnAttackAnimationEnded -= OnAttackAnimationEnded;

            _playerView.CollisionDetector.OnCollisionEntered -= OnCollisionEntered;
            _playerView.CollisionDetector.OnCollisionExited -= OnCollisionExit;

            _playerView.SideDetectorView.OnTriggerEntered -= InvokePlayerSideCheck;
            _playerView.SideDetectorView.OnTriggerExited -= InvokePlayerSideCheck;

            _playerModel.OnWin -= _stateMachineProxy.ChangeState<WinState>;
            _playerModel.OnLose -= _stateMachineProxy.ChangeState<LoseState>;

            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private void InvokePlayerSideCheck(Collider collider)
        {
            if (collider.GetComponent<CollisionDetectorView>())
            {
                _fightSceneModel.InvokePlayerSideCheck(_playerModel);
            }
        }

        private void OnAttackAnimationEnded()
        {
            _playerModel.IsAttacking.Value = false;
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

        private void OnMoveForwardInput()
        {
            _runStateModel.SetData(_inputModelsContainer.MoveForward.Key, DirectionType.Forward,
                PlayerAnimatorData.Forward, typeof(DashForwardState));

            _stateMachineProxy.ChangeState<RunState>();
        }

        private void OnMoveBackwardInput()
        {
            _runStateModel.SetData(_inputModelsContainer.MoveBackward.Key, DirectionType.Backward,
                PlayerAnimatorData.Backward, typeof(DashBackwardState));

            _stateMachineProxy.ChangeState<RunState>();
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