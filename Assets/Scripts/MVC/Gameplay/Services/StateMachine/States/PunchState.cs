using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class PunchState : CommonStates.AttackState
    {
        public PunchState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerView.Animator.SetBool(PlayerAnimatorData.IsPunching, true);
            StateModel.PlayerModel.IsAttacking.Value = true;
            StateModel.PlayerAttackModel.Damage = StateModel.PlayerModel.PunchDamage;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerView.Animator.SetBool(PlayerAnimatorData.IsPunching, false);
        }
    }
}