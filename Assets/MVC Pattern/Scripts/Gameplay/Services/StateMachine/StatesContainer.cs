using System.Collections.Generic;
using System.Linq;
using MVC.StateMachine.States;

namespace MVC.Models
{
    public class StatesContainer
    {
        public IEnumerable<IState> States { get; }

        public StatesContainer(IEnumerable<IState> states)
        {
            States = states;
        }

        public IState GetStateByType<T>() where T : IState
        {
            return States.FirstOrDefault(s => s.GetType() == typeof(T));
        }
    }
}