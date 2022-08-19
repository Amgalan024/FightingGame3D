using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class BlockState : State
    {
        public BlockState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            PlayerView.Animator.SetBool(PlayerAnimatorData.IsBlocking, true);
        }

        public override void Exit()
        {
            PlayerView.Animator.SetBool(PlayerAnimatorData.IsBlocking, false);
        }

        public override void OnTriggerExit(Collider collider)
        {
            base.OnTriggerExit(collider);

            if (collider.GetComponent<PlayerAttackHitBoxView>())
            {
                if (PlayerView.Animator.GetBool(PlayerAnimatorData.IsCrouching))
                {
                    StateMachineModel.ChangeState(StateModel.StatesContainer.CrouchState);
                }
                else
                {
                    if (StateModel.PlayerModel.MovementSpeed < 0.1)
                    {
                        StateMachineModel.ChangeState(StateModel.StatesContainer.IdleState);
                    }
                    else if (PlayerView.Animator.GetFloat(PlayerAnimatorData.Forward) > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateMachineModel.ChangeState(StateModel.StatesContainer.MoveForwardState);
                    }
                    else if (PlayerView.Animator.GetFloat(PlayerAnimatorData.Backward) > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateMachineModel.ChangeState(StateModel.StatesContainer.MoveBackwardState);
                    }
                }
            }
        }
    }
}