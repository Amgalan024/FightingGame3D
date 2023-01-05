using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public class PunchState : CommonStates.AttackState
    {
        public PunchState(PlayerContainer playerContainer) : base(playerContainer)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsPunching, true);

            PlayerContainer.AttackModel.Damage = PlayerContainer.Model.PunchDamage;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsPunching, false);
        }
    }
}