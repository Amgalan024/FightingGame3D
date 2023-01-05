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
            PlayerContainer.InputActionModelsContainer.SetAllInputActionModelFilters(false);
            PlayStunAnimationAsync(Cts.Token).Forget();
        }

        public void Exit()
        {
        }

        private async UniTask PlayStunAnimationAsync(CancellationToken token)
        {
            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsStunned, true);

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);

            StateMachine.ChangeState<IdleState>();
        }
    }
}