using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Configs.Enums;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class RunState : State, IFixedTickState, ITriggerEnterState
    {
        private readonly RunStateModel _runStateModel;
        private readonly JumpStateModel _jumpStateModel;
        private readonly FallStateModel _fallStateModel;

        private CancellationTokenSource _dashCts;

        public RunState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage,
            RunStateModel runStateModel, JumpStateModel jumpStateModel, FallStateModel fallStateModel) : base(
            stateModel, playerView, storage)
        {
            _runStateModel = runStateModel;
            _jumpStateModel = jumpStateModel;
            _fallStateModel = fallStateModel;
        }

        public override void Enter()
        {
            base.Enter();

            var animationData = StateModel.CharacterConfig.PlayerAnimationData;

            _jumpStateModel.Direction = PlayerView.GetPlayerDirection();
            _jumpStateModel.JumpTweenVectorData =
                animationData.GetTweenDataByDirection(animationData.JumpTweenData, _runStateModel.DirectionType);

            _fallStateModel.Direction = PlayerView.GetPlayerDirection();
            _fallStateModel.FallTweenVectorData =
                animationData.GetTweenDataByDirection(animationData.FallTweenData, _runStateModel.DirectionType);

            _dashCts?.Cancel();
            _dashCts?.Dispose();
            _dashCts = new CancellationTokenSource();

            InputDash();

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetJumpInputActionsFilter(true);

            PlayerView.IdleToMoveAnimationAsync(_runStateModel.AnimationHash, Token).Forget();
        }

        public override void Exit()
        {
            base.Exit();

            PlayerView.MoveToIdleAnimationAsync(_runStateModel.AnimationHash, Token).Forget();
        }

        void IFixedTickState.OnFixedTick()
        {
            if (!Input.GetKey(_runStateModel.InputKey))
            {
                PlayerView.Rigidbody.velocity = Vector3.zero;

                StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
            }
            else
            {
                var velocity = PlayerView.Rigidbody.velocity;

                velocity.x = (int) _runStateModel.DirectionType * StateModel.PlayerModel.MaxMovementSpeed *
                             PlayerView.GetPlayerDirection();

                PlayerView.Rigidbody.velocity = velocity;
            }
        }

        void ITriggerEnterState.OnTriggerEnter(Collider collider)
        {
            if (_runStateModel.DirectionType == DirectionType.Backward)
            {
                HandleBlock(collider);
            }
        }

        private void InputDash()
        {
            UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _dashCts.Token).ContinueWith(_dashCts.Cancel)
                .Forget();

            UniTask.WaitUntil(() => Input.GetKeyDown(_runStateModel.InputKey),
                    cancellationToken: _dashCts.Token)
                .ContinueWith(() => StateModel.StateMachineProxy.ChangeState(_runStateModel.DashStateType))
                .Forget();
        }
    }
}