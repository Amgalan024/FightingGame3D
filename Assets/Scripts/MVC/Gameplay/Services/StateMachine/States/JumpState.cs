using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class JumpState : State
    {
        private CancellationTokenSource _fallCts;

        public JumpState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);

            if (!CheckPreviousStateIsAttackState())
            {
                var velocity = PlayerView.Rigidbody.velocity;

                velocity.y = StateModel.PlayerModel.JumpForce;

                PlayerView.Rigidbody.velocity = velocity;

                StateModel.PlayerModel.CurrentJumpCount++;
            }

            if (StateModel.PlayerModel.CurrentJumpCount < 2)
            {
                StateModel.InputActionModelsContainer.SetJumpInputActionsFilter(true);
            }

            _fallCts = new CancellationTokenSource();

            AwaitFall(_fallCts.Token).Forget();
        }

        public override void Exit()
        {
            _fallCts.Cancel();
            _fallCts.Dispose();
        }

        private async UniTask AwaitFall(CancellationToken token)
        {
            await UniTask.WaitUntil(() => PlayerView.Rigidbody.velocity.y <= 0, cancellationToken: token);
            StateModel.StateMachineProxy.ChangeState(typeof(FallState));
        }

        private bool CheckPreviousStateIsAttackState()
        {
            var memberInfo = StateModel.StateMachineModel.PreviousState.GetType().BaseType;

            if (memberInfo != null && memberInfo.IsEquivalentTo(typeof(AttackState)))
            {
                return true;
            }

            return false;
        }
    }
}