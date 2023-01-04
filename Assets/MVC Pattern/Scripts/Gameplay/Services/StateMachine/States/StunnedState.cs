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
        public IStateMachineProxy StateMachineProxy { get; }

        public StunnedState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
        }

        public void Enter()
        {
            PlayerContainer.InputActionModelsContainer.SetAllInputActionModels(false);
            PlayStunnedAnimationAsync(Cts.Token).Forget();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        private async UniTask PlayStunnedAnimationAsync(CancellationToken token)
        {
            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.IsStunned, true);

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);

            StateMachineProxy.ChangeState<IdleState>();
        }
    }
}