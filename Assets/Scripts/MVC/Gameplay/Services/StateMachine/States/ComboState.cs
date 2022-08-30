using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class ComboState : CommonStates.AttackState
    {
        public string Name { set; get; }
        public int Damage { set; get; }

        public ComboState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerView.Animator.Play(Name);
            PlayerView.Animator.SetBool(Name, true);
            StateModel.PlayerAttackModel.Damage = Damage;
            StateModel.PlayerModel.IsDoingCombo.Value = true;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerView.Animator.SetBool(Name, false);
            StateModel.PlayerModel.IsDoingCombo.Value = false;
        }
    }
}