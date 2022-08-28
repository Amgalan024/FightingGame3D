using System;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

namespace MVC.StateMachine.States.CommonStates
{
    public class AttackState : State
    {
        private IDisposable _exitStateSubscription;

        public AttackState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
            _exitStateSubscription = StateModel.PlayerModel.IsAttacking.Subscribe(ExitAttackState);
        }

        public override void Exit()
        {
            _exitStateSubscription?.Dispose();
        }

        private void ExitAttackState(bool isAttacking)
        {
            if (isAttacking)
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