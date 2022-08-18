using System;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Models;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class PunchState : State
    {
        private IDisposable _exitAttackSubscription;

        public PunchState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            PlayerView.Animator.SetBool("IsPunching", true);
            StateModel.PlayerModel.IsAttacking.Value = true;
            StateModel.PlayerAttackModel.Damage = StateModel.PlayerModel.PunchDamage;

            _exitAttackSubscription = StateModel.PlayerModel.IsAttacking.Subscribe(TryExitAttackState);
        }

        public override void Exit()
        {
            _exitAttackSubscription.Dispose();
            PlayerView.Animator.SetBool("IsPunching", false);
        }

        private void TryExitAttackState(bool isAttacking)
        {
            if (!isAttacking)
            {
                if (PlayerView.Animator.GetBool("IsCrouching"))
                {
                    StateMachineModel.ChangeState(StateModel.StatesContainer.CrouchState);
                }
                else
                {
                    if (StateModel.PlayerModel.MovementSpeed < 0.1)
                    {
                        StateMachineModel.ChangeState(StateModel.StatesContainer.IdleState);
                    }
                    else if (PlayerView.Animator.GetFloat("Forward") > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateMachineModel.ChangeState(StateModel.StatesContainer.MoveForwardState);
                    }
                    else if (PlayerView.Animator.GetFloat("Backward") > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateMachineModel.ChangeState(StateModel.StatesContainer.MoveBackwardState);
                    }
                }
            }
        }
    }
}