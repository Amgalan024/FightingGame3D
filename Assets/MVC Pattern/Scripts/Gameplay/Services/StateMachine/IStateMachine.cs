using System;
using MVC.StateMachine.States;

namespace MVC_Pattern.Scripts.Gameplay.Services.StateMachine
{
    public interface IStateMachine
    {
        void ChangeState<T>() where T : IState;
        void ChangeState(Type type);
    }
}