using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class StunnedState : State
    {
        public StunnedState(StateModel stateModel, PlayerView playerView) : base(stateModel, playerView)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);
            PlayStunnedAnimationAsync(Cts.Token).Forget();
        }

        private async UniTask PlayStunnedAnimationAsync(CancellationToken token)
        {
            PlayerView.Animator.SetBool(PlayerAnimatorData.IsStunned, true);

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);

            StateModel.StateMachineProxy.ChangeState<IdleState>();
        }
    }
}