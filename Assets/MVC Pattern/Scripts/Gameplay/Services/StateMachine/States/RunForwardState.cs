using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class RunForwardState : State
    {
        private CancellationTokenSource _dashCts;

        public RunForwardState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(
            stateModel, playerView, storage)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _dashCts?.Dispose();
            _dashCts = new CancellationTokenSource();

            InputDash();

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetJumpInputActionsFilter(true);

            PlayerView.IdleToMoveAnimationAsync(PlayerAnimatorData.Forward, Token).Forget();
        }

        public override void OnFixedTick()
        {
            if (!Input.GetKey(StateModel.InputModelsContainer.MoveForward.Key))
            {
                PlayerView.Rigidbody.velocity = Vector3.zero;

                StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
            }
            else
            {
                var velocity = PlayerView.Rigidbody.velocity;

                velocity.x = StateModel.PlayerModel.MaxMovementSpeed * PlayerView.transform.localScale.z;

                PlayerView.Rigidbody.velocity = velocity;
            }
        }

        public override void Exit()
        {
            base.Exit();

            PlayerView.MoveToIdleAnimationAsync(PlayerAnimatorData.Forward, Token).Forget();
        }

        private void InputDash()
        {
            UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: Token).ContinueWith(_dashCts.Cancel)
                .Forget();

            UniTask.WaitUntil(() => Input.GetKeyDown(StateModel.InputModelsContainer.MoveForward.Key),
                    cancellationToken: _dashCts.Token)
                .ContinueWith(() => StateModel.StateMachineProxy.ChangeState(typeof(DashForwardState)))
                .Forget();
        }
    }
}