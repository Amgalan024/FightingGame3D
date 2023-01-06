using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Configs.Enums;
using MVC.Gameplay.Models.Player;
using MVC.Utils.Disposable;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class RunState : DisposableWithCts, IPlayerState, IFixedTickState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        private readonly RunStateModel _runStateModel;
        private readonly JumpStateModel _jumpStateModel;

        private CancellationTokenSource _dashCts;

        public RunState(PlayerContainer playerContainer, RunStateModel runStateModel, JumpStateModel jumpStateModel)
        {
            PlayerContainer = playerContainer;
            _runStateModel = runStateModel;
            _jumpStateModel = jumpStateModel;
        }

        public void Enter()
        {
            ConfigureInputActionFilters();

            ConfigureJumpModel();

            InputDash();

            PlayerContainer.View.PlayIdleToMoveAnimationAsync(_runStateModel.AnimationHash, Token).Forget();
        }

        public void Exit()
        {
            PlayerContainer.View.PlayMoveToIdleAnimationAsync(_runStateModel.AnimationHash, Token).Forget();
        }

        void IFixedTickState.OnFixedTick()
        {
            HandleRunKeyInput();
        }

        private void ConfigureInputActionFilters()
        {
            if (_runStateModel.MovementType == MovementType.Backward)
            {
                PlayerContainer.InputModelsContainer.SetBlockInputActionFilters(true);
            }

            PlayerContainer.InputModelsContainer.SetAllInputActionModelFilters(false);

            PlayerContainer.InputModelsContainer.SetAttackInputActionFilters(true);
            PlayerContainer.InputModelsContainer.SetJumpInputActionFilter(true);
        }

        private void ConfigureJumpModel()
        {
            var animationData = PlayerContainer.AnimationData;

            _jumpStateModel.MovementType = _runStateModel.MovementType;
            _jumpStateModel.Direction = PlayerContainer.View.GetPlayerDirection();
            _jumpStateModel.JumpTweenConfig =
                animationData.GetTweenDataByMovementType(animationData.JumpTweenData, _runStateModel.MovementType);
        }

        private void InputDash()
        {
            _dashCts?.Cancel();
            _dashCts?.Dispose();
            _dashCts = new CancellationTokenSource();

            UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _dashCts.Token).ContinueWith(_dashCts.Cancel)
                .Forget();

            UniTask.WaitUntil(() => Input.GetKeyDown(_runStateModel.InputKey), cancellationToken: _dashCts.Token)
                .ContinueWith(() =>
                    {
                        switch (_runStateModel.MovementType)
                        {
                            case MovementType.Forward:
                                StateMachine.ChangeState<DashForwardState>();
                                break;
                            case MovementType.Backward:
                                StateMachine.ChangeState<DashBackwardState>();
                                break;
                        }
                    }
                )
                .Forget();
        }

        private void HandleRunKeyInput()
        {
            var playerView = PlayerContainer.View;

            if (!Input.GetKey(_runStateModel.InputKey))
            {
                playerView.Rigidbody.velocity = Vector3.zero;

                StateMachine.ChangeState<IdleState>();
            }
            else
            {
                var velocity = playerView.Rigidbody.velocity;

                velocity.x = (int) _runStateModel.MovementType * PlayerContainer.Model.MaxMovementSpeed *
                             playerView.GetPlayerDirection();

                playerView.Rigidbody.velocity = velocity;
            }
        }
    }
}