using System;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class FallState : State
    {
        private IDisposable _fallSubscription;

        public FallState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            _fallSubscription = StateModel.PlayerModel.IsGrounded.Subscribe(OnFall);

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);
        }

        public override void Exit()
        {
            _fallSubscription.Dispose();
        }

        private void OnFall(bool isGrounded)
        {
            if (isGrounded)
            {
                StateModel.StatesContainer.JumpState.JumpCount = 0;
                if (StateModel.PlayerModel.MovementSpeed < 0.1)
                {
                    StateMachineModel.ChangeState(StateModel.StatesContainer.IdleState);
                }
                else if (PlayerView.Animator.GetFloat(PlayerAnimatorData.Forward) > 0.1)
                {
                    StateMachineModel.ChangeState(StateModel.StatesContainer.MoveForwardState);
                }
                else if (PlayerView.Animator.GetFloat(PlayerAnimatorData.Backward) > 0.1)
                {
                    StateMachineModel.ChangeState(StateModel.StatesContainer.MoveBackwardState);
                }
            }
        }
    }
}