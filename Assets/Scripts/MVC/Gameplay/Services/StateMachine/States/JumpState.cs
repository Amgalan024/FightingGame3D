using Cysharp.Threading.Tasks;
using MVC.Gameplay.Models;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class JumpState : State
    {
        public int JumpCount { set; get; }

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
                JumpAsync().ContinueWith(() => StateMachineModel.ChangeState(StateModel.StatesContainer.FallState));
                JumpCount++;
            }
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

        private async UniTask JumpAsync()
        {
        }
    }
}