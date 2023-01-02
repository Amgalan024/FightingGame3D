using System;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.Gameplay.Models.StateMachineModels
{
    public class StateMachineProxy : IStateMachine
    {
        public event Action<Type> OnStateChanged;

        public void ChangeState<T>() where T : IState
        {
            OnStateChanged?.Invoke(typeof(T));
        }

        public void ChangeState(Type type)
        {
            OnStateChanged?.Invoke(type);
        }
    }
}