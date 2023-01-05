using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using MVC.Configs.Enums;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.StateMachine.States;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerStatesController : IStartable, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private readonly InputActionModelsContainer _inputActionModelsContainer;
        private readonly FightSceneModel _fightSceneModel;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly PlayerContainer _opponentContainer;
        private readonly PlayerContainer _playerContainer;
        private readonly InputModelsContainer _inputModelsContainer;

        private readonly Transform _parent;

        private readonly List<IDisposable> _subscriptions = new List<IDisposable>(5);

        private readonly RunStateModel _runStateModel;

        public PlayerStatesController(PlayerContainer playerContainer, IStateMachine stateMachine,
            FightSceneModel fightSceneModel, LifetimeScope lifetimeScope, RunStateModel runStateModel)
        {
            _playerContainer = playerContainer;
            _playerModel = playerContainer.Model;
            _playerView = playerContainer.View;
            _inputModelsContainer = playerContainer.InputModelsContainer;
            _inputActionModelsContainer = playerContainer.InputActionModelsContainer;
            _opponentContainer = playerContainer.OpponentContainer;
            _stateMachine = stateMachine;
            _fightSceneModel = fightSceneModel;
            _runStateModel = runStateModel;
            _parent = lifetimeScope.transform;
        }

        void IStartable.Start()
        {
            _fightSceneModel.InvokePlayerSideCheck(_playerContainer);

            _playerView.SetParent(_parent);

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
            _playerView.OnAttackAnimationEnded += OnAttackAnimationEnded;

            _playerView.MainTriggerDetector.OnTriggerEntered += HandleBlock;

            _playerView.CollisionDetector.OnCollisionEntered += OnCollisionEntered;
            _playerView.CollisionDetector.OnCollisionExited += OnCollisionExit;

            _playerView.SideDetectorView.OnTriggerEntered += InvokePlayerSideCheck;
            _playerView.SideDetectorView.OnTriggerExited += InvokePlayerSideCheck;

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
            _playerView.OnAttackAnimationEnded -= OnAttackAnimationEnded;

            _playerView.MainTriggerDetector.OnTriggerEntered -= HandleBlock;

            _playerView.CollisionDetector.OnCollisionEntered -= OnCollisionEntered;
            _playerView.CollisionDetector.OnCollisionExited -= OnCollisionExit;

            _playerView.SideDetectorView.OnTriggerEntered -= InvokePlayerSideCheck;
            _playerView.SideDetectorView.OnTriggerExited -= InvokePlayerSideCheck;

            _playerModel.OnWin -= _stateMachine.ChangeState<WinState>;
            _playerModel.OnLose -= _stateMachine.ChangeState<LoseState>;

            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private void OnAttackAnimationEnded()
        {
            _playerModel.IsAttacking.Value = false;
        }

        private void HandleBlock(Collider collider)
        {
            if (collider.TryGetComponent(out TriggerDetectorView attackHitBox) &&
                attackHitBox == _opponentContainer.AttackHitBox)
            {
                if (_playerModel.IsBlocking)
                {
                    _stateMachine.ChangeState<BlockState>();
                }
                else
                {
                    _playerModel.InvokePlayerAttacked(attackHitBox);
                    _stateMachine.ChangeState<StunnedState>();
                }
            }
        }

        private void InvokePlayerSideCheck(Collider collider)
        {
            if (collider.GetComponent<PlayerView>())
            {
                _fightSceneModel.InvokePlayerSideCheck(_playerContainer);
            }
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