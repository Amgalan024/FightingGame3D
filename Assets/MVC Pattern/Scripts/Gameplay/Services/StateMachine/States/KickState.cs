using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class KickState : CommonStates.AttackState
    {
        public KickState(StateModel stateModel, PlayerView playerView) : base(stateModel, playerView)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerView.Animator.SetBool(PlayerAnimatorData.IsKicking, true);

            StateModel.PlayerAttackModel.Damage = StateModel.PlayerModel.KickDamage;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerView.Animator.SetBool(PlayerAnimatorData.IsKicking, false);
        }
    }
}