using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class BlockState : State, ITriggerExitState
    {
        public BlockState(StateModel stateModel, PlayerView playerView) : base(stateModel, playerView)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerView.Animator.SetBool(PlayerAnimatorData.IsBlocking, true);
        }

        public override void Exit()
        {
            base.Exit();

            PlayerView.Animator.SetBool(PlayerAnimatorData.IsBlocking, false);
        }

        void ITriggerExitState.OnTriggerExit(Collider collider)
        {
            ExitBlockState(collider);
        }

        private void ExitBlockState(Collider collider)
        {
            if (collider.GetComponent<TriggerDetectorView>() ==
                StateModel.OpponentContainer.PlayerAttackHitBox)
            {
                if (PlayerView.Animator.GetBool(PlayerAnimatorData.IsCrouching))
                {
                    StateModel.StateMachineProxy.ChangeState<CrouchState>();
                }
                else
                {
                    StateModel.StateMachineProxy.ChangeState<IdleState>();
                }
            }
        }
    }
}