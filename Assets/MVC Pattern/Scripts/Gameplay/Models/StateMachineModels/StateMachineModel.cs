using MVC.StateMachine.States;

namespace MVC.Gameplay.Models
{
    public class StateMachineModel
    {
        public IState PreviousState { get; set; }
        public IState CurrentState { get; private set; }
        public IFixedTickState FixedTickState { get; private set; }
        public ITriggerEnterState TriggerEnterState { get; private set; }
        public ITriggerExitState TriggerExitState { get; private set; }

        public void SetCurrentState(IState state)
        {
            CurrentState = state;
            ExtractStateInterfaces(CurrentState);
        }

        public bool CheckPreviousStateType<T>() where T : IState
        {
            var memberInfo = PreviousState.GetType();

            if (memberInfo.IsEquivalentTo(typeof(T)))
            {
                return true;
            }

            return false;
        }

        private void ExtractStateInterfaces(IState state)
        {
            FixedTickState = null;
            TriggerEnterState = null;
            TriggerExitState = null;

            if (state is IFixedTickState fixedTickState)
            {
                FixedTickState = fixedTickState;
            }

            if (state is ITriggerEnterState triggerEnterState)
            {
                TriggerEnterState = triggerEnterState;
            }

            if (state is ITriggerExitState triggerExitState)
            {
                TriggerExitState = triggerExitState;
            }
        }
    }
}