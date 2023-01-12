using System;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class KickState : CommonStates.AttackState
    {
        public KickState(PlayerContainer playerContainer) : base(playerContainer)
        {
        }

        public override void Enter()
        {
            base.Enter();

            PlayerContainer.View.AttackHitBoxView.OnTriggerEntered += OnAttackHit;
            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsKicking, true);

            PlayerContainer.AttackModel.Damage = PlayerContainer.Model.KickDamage;
        }

        public override void Exit()
        {
            base.Exit();

            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsKicking, false);
        }

        private void OnAttackHit(Collider collider)
        {
            if (collider == PlayerContainer.OpponentContainer.View.MainTriggerDetector.Collider)
            {
                PauseEffectAsync().Forget();
            }

            PlayerContainer.View.AttackHitBoxView.OnTriggerEntered -= OnAttackHit;
        }

        private async UniTask PauseEffectAsync()
        {
            PlayerContainer.View.Animator.speed = 0;

            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

            PlayerContainer.View.Animator.speed = 1;
        }
    }
}