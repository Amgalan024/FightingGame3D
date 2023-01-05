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
        public IStateMachineProxy StateMachineProxy { get; }

        public BlockState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
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
                    StateMachineProxy.ChangeState<CrouchState>();
                }
                else
                {
                    StateMachineProxy.ChangeState<IdleState>();
                }
            }
        }
    }
}