using System;
using System.Collections.Generic;
using System.Linq;
using MVC.StateMachine.States;

namespace MVC.Models
{
    public class StatesContainer
    {
        private readonly IEnumerable<IState> _states;

        public StatesContainer(IEnumerable<IState> states)
        {
            _states = states;
        }

        public IState GetStateByType(Type type)
        {
            return _states.FirstOrDefault(s => s.GetType() == type);
        }
    }
}