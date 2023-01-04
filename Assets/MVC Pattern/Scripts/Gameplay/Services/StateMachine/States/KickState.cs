using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public class KickState : CommonStates.AttackState
    {
        public KickState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy) : base(playerContainer,
            stateMachineProxy)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsKicking, true);

            PlayerContainer.AttackModel.Damage = PlayerContainer.Model.KickDamage;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsKicking, false);
        }
    }
}