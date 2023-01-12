using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC.Utils.Disposable;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public class StunnedState : DisposableWithCts, IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        public StunnedState(PlayerContainer playerContainer)
        {
            PlayerContainer = playerContainer;
        }

        public void Enter()
        {
            PlayerContainer.InputFilterModelsContainer.SetAllInputActionModelFilters(false);
            PlayerContainer.View.Animator.SetTrigger(PlayerAnimatorData.StunnedTrigger);
            PlayerContainer.View.OnStunAnimationEnded += GoToIdleState;
            PlayerContainer.Model.OnLose += OnLose;
        }

        public void Exit()
        {
        }

        private void OnLose()
        {
            PlayerContainer.View.OnStunAnimationEnded -= GoToIdleState;
            PlayerContainer.Model.OnLose -= OnLose;
        }

        private void GoToIdleState()
        {
            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsStunned, false);

            StateMachine.ChangeState<IdleState>();
            
            PlayerContainer.View.OnStunAnimationEnded -= GoToIdleState;
        }
    }
}