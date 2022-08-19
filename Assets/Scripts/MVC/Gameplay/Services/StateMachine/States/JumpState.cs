using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Models;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class JumpState : State
    {
        public int JumpCount { set; get; }

        private CancellationTokenSource _fallCts;

        public JumpState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
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

                JumpCount++;
            }

            if (JumpCount < 2)
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
            StateMachineModel.ChangeState(StateModel.StatesContainer.FallState);
        }

        private bool CheckPreviousStateIsAttackState()
        {
            var memberInfo = StateMachineModel.PreviousState.GetType().BaseType;

            if (memberInfo != null &&
                memberInfo.IsEquivalentTo(StateModel.StatesContainer.PunchState.GetType().BaseType))
            {
                return true;
            }

            return false;
        }
    }
}