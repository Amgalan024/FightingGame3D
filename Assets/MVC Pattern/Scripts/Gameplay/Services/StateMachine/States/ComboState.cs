using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public class ComboState : CommonStates.AttackState
    {
        public string Name { set; get; }
        public int Damage { set; get; }

        public ComboState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy) : base(playerContainer,
            stateMachineProxy)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerContainer.View.Animator.Play(Name);
            PlayerContainer.View.Animator.SetBool(Name, true);
            PlayerContainer.AttackModel.Damage = Damage;
            PlayerContainer.Model.IsDoingCombo.Value = true;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerContainer.View.Animator.SetBool(Name, false);
            PlayerContainer.Model.IsDoingCombo.Value = false;
        }
    }
}