using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;

namespace MVC.StateMachine.States
{
    public class ComboState : CommonStates.AttackState
    {
        private readonly ComboStateModel _comboStateModel;

        public ComboState(PlayerContainer playerContainer, ComboStateModel comboStateModel) : base(playerContainer)
        {
            _comboStateModel = comboStateModel;
        }

        public override void Enter()
        {
            base.Enter();

            PlayerContainer.View.Animator.Play(_comboStateModel.Name);
            PlayerContainer.View.Animator.SetBool(_comboStateModel.Name, true);
            PlayerContainer.AttackModel.Damage = _comboStateModel.Damage;
            PlayerContainer.Model.IsDoingCombo.Value = true;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerContainer.View.Animator.SetBool(_comboStateModel.Name, false);
            PlayerContainer.Model.IsDoingCombo.Value = false;
        }
    }
}