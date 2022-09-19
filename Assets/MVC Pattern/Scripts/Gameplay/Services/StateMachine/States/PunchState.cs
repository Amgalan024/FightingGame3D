using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class PunchState : CommonStates.AttackState
    {
        public PunchState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerView.Animator.SetBool(PlayerAnimatorData.IsPunching, true);

            StateModel.PlayerAttackModel.Damage = StateModel.PlayerModel.PunchDamage;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerView.Animator.SetBool(PlayerAnimatorData.IsPunching, false);
        }
    }
}