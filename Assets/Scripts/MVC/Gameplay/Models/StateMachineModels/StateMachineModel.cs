using System;
using MVC.Models;
using MVC.StateMachine.States;

namespace MVC.Gameplay.Models
{
    public class StateMachineModel
    {
        public event Action<IState> OnStateChanged;
        public IState CurrentState { get; set; }
        public IState PreviousState { get; set; }

        public void ChangeState(IState newState)
        {
            OnStateChanged?.Invoke(newState);
        }
    }
}