using MVC.Gameplay.Models;
using MVC.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class BlockState : State
    {
        private static readonly int IsBlocking = Animator.StringToHash("IsBlocking");
        private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");
        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int Backward = Animator.StringToHash("Backward");


        public BlockState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            PlayerView.Animator.SetBool(IsBlocking, true);
        }

        public override void Exit()
        {
            PlayerView.Animator.SetBool(IsBlocking, false);
        }

        public override void OnTriggerExit(Collider collider)
        {
            base.OnTriggerExit(collider);

            if (collider.GetComponent<PlayerAttackHitBoxView>())
            {
                if (PlayerView.Animator.GetBool(IsCrouching))
                {
                    StateMachineModel.ChangeState(StateModel.StatesContainer.CrouchState);
                }
                else
                {
                    if (StateModel.PlayerModel.MovementSpeed < 0.1)
                    {
                        StateMachineModel.ChangeState(StateModel.StatesContainer.IdleState);
                    }
                    else if (PlayerView.Animator.GetFloat(Forward) > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateMachineModel.ChangeState(StateModel.StatesContainer.MoveForwardState);
                    }
                    else if (PlayerView.Animator.GetFloat(Backward) > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateMachineModel.ChangeState(StateModel.StatesContainer.MoveBackwardState);
                    }
                }
            }
        }
    }
}