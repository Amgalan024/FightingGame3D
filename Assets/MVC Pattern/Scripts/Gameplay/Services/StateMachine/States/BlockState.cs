using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class BlockState : IPlayerState, ITriggerExitState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        public BlockState(PlayerContainer playerContainer, IStateMachine stateMachine)
        {
            PlayerContainer = playerContainer;
            StateMachine = stateMachine;
        }

        public void Enter()
        {
            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsBlocking, true);
        }

        public void Exit()
        {
            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsBlocking, false);
        }

        void ITriggerExitState.OnTriggerExit(Collider collider)
        {
            ExitBlockState(collider);
        }

        private void ExitBlockState(Collider collider)
        {
            if (collider.GetComponent<TriggerDetectorView>() == PlayerContainer.OpponentContainer.AttackHitBox)
            {
                if (PlayerContainer.View.Animator.GetBool(PlayerAnimatorData.IsCrouching))
                {
                    StateMachine.ChangeState<CrouchState>();
                }
                else
                {
                    StateMachine.ChangeState<IdleState>();
                }
            }
        }
    }
}