using MVC.Gameplay.Models;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private readonly StateMachineModel _stateMachineModel;
        private readonly StatesContainer _statesContainer;

        public StateMachine(StateMachineModel stateMachineModel, StatesContainer statesContainer)
        {
            _stateMachineModel = stateMachineModel;
            _statesContainer = statesContainer;
        }

        public void ChangeState<T>() where T : IState
        {
            var newState = _statesContainer.GetStateByType<T>();

            ChangeState(newState);
        }

        private void ChangeState(IState newState)
        {
            _stateMachineModel.PreviousState = _stateMachineModel.CurrentState;
            Debug.Log($" Exited {_stateMachineModel.CurrentState?.GetType()}");
            _stateMachineModel.CurrentState?.Exit();
            _stateMachineModel.SetCurrentState(newState);

            newState.Enter();
            Debug.Log($" Entered {_stateMachineModel.CurrentState.GetType()}");
        }
    }
}