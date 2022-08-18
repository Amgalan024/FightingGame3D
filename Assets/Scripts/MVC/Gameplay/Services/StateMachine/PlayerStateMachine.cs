using System;
using MVC.Gameplay.Models;
using MVC.StateMachine.States;

namespace MVC.StateMachine
{
    public class PlayerStateMachine : IDisposable
    {
        private readonly StateMachineModel _stateMachineModel;

        public PlayerStateMachine(StateMachineModel stateMachineModel)
        {
            _stateMachineModel = stateMachineModel;
        }

        public void StartStateMachine(IState startingState)
        {
            _stateMachineModel.CurrentState = startingState;
            startingState.Enter();
            _stateMachineModel.OnStateChanged += ChangeState;
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

        public void Dispose()
        {
            _stateMachineModel.OnStateChanged -= ChangeState;
        }
    }
}