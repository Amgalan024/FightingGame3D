using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class BlockState : State
    {
        public BlockState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
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

        public override void OnTriggerExit(Collider collider)
        {
            base.OnTriggerExit(collider);

            if (collider.GetComponent<PlayerAttackHitBoxView>())
            {
                if (PlayerView.Animator.GetBool(PlayerAnimatorData.IsCrouching))
                {
                    StateModel.StateMachineProxy.ChangeState(typeof(CrouchState));
                }
                else
                {
                    if (StateModel.PlayerModel.MovementSpeed < 0.1)
                    {
                        StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
                    }
                    else if (PlayerView.Animator.GetFloat(PlayerAnimatorData.Forward) > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateModel.StateMachineProxy.ChangeState(typeof(RunForwardState));
                    }
                    else if (PlayerView.Animator.GetFloat(PlayerAnimatorData.Backward) > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateModel.StateMachineProxy.ChangeState(typeof(RunBackwardState));
                    }
                }
            }
        }
    }
}