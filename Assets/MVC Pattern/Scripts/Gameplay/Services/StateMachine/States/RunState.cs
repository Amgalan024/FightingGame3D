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
        public IStateMachineProxy StateMachineProxy { get; }

        private readonly RunStateModel _runStateModel;
        private readonly JumpStateModel _jumpStateModel;
        private readonly FallStateModel _fallStateModel;

        private CancellationTokenSource _dashCts;

        public RunState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy,
            RunStateModel runStateModel, JumpStateModel jumpStateModel, FallStateModel fallStateModel)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
            _runStateModel = runStateModel;
            _jumpStateModel = jumpStateModel;
            _fallStateModel = fallStateModel;
        }

        public void Enter()
        {
            if (_runStateModel.DirectionType == DirectionType.Backward)
            {
                PlayerContainer.InputActionModelsContainer.SetBlockInputActionsFilter(true);
            }

            var animationData = PlayerContainer.AnimationData;
            var playerView = PlayerContainer.View;

            _jumpStateModel.Direction = playerView.GetPlayerDirection();
            _jumpStateModel.JumpTweenConfig =
                animationData.GetTweenDataByDirection(animationData.JumpTweenData, _runStateModel.DirectionType);

            _fallStateModel.Direction = playerView.GetPlayerDirection();
            _fallStateModel.FallTweenConfig =
                animationData.GetTweenDataByDirection(animationData.FallTweenData, _runStateModel.DirectionType);

            _dashCts?.Cancel();
            _dashCts?.Dispose();
            _dashCts = new CancellationTokenSource();

            InputDash();

            PlayerContainer.InputActionModelsContainer.SetAllInputActionModels(false);

            PlayerContainer.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            PlayerContainer.InputActionModelsContainer.SetJumpInputActionsFilter(true);

            playerView.IdleToMoveAnimationAsync(_runStateModel.AnimationHash, Token).Forget();
        }

        public void Exit()
        {
            PlayerContainer.View.MoveToIdleAnimationAsync(_runStateModel.AnimationHash, Token).Forget();
        }

        void IFixedTickState.OnFixedTick()
        {
            var playerView = PlayerContainer.View;

            if (!Input.GetKey(_runStateModel.InputKey))
            {
                playerView.Rigidbody.velocity = Vector3.zero;

                StateMachineProxy.ChangeState<IdleState>();
            }
            else
            {
                var velocity = playerView.Rigidbody.velocity;

                velocity.x = (int) _runStateModel.DirectionType * PlayerContainer.Model.MaxMovementSpeed *
                             playerView.GetPlayerDirection();

                playerView.Rigidbody.velocity = velocity;
            }
        }

        private void InputDash()
        {
            UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _dashCts.Token).ContinueWith(_dashCts.Cancel)
                .Forget();

            UniTask.WaitUntil(() => Input.GetKeyDown(_runStateModel.InputKey), cancellationToken: _dashCts.Token)
                .ContinueWith(() =>
                    {
                        switch (_runStateModel.DirectionType)
                        {
                            case DirectionType.Forward:
                                StateMachineProxy.ChangeState<DashForwardState>();
                                break;
                            case DirectionType.Backward:
                                StateMachineProxy.ChangeState<DashBackwardState>();
                                break;
                        }
                    }
                )
                .Forget();
        }
    }
}