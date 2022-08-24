using MVC.Gameplay.Models;
using MVC.StateMachine.States;

namespace MVC.StateMachine
{
    public class StateMachine
    {
        private readonly StateMachineModel _stateMachineModel;

        public StateMachine(StateMachineModel stateMachineModel)
        {
            _stateMachineModel = stateMachineModel;
        }

        public void StartStateMachine(IState startingState)
        {
            _stateMachineModel.CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(IState newState)
        {
            _stateMachineModel.PreviousState = _stateMachineModel.CurrentState;

            _stateMachineModel.CurrentState.IsActive = false;
            _stateMachineModel.CurrentState.Exit();

            _stateMachineModel.CurrentState = newState;

            newState.IsActive = true;
            newState.Enter();
        }
    }
}