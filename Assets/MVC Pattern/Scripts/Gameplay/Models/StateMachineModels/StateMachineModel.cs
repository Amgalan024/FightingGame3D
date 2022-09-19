using System;
using MVC.StateMachine.States;

namespace MVC.Gameplay.Models
{
    public class StateMachineModel
    {
        public IState CurrentState { get; set; }
        public IState PreviousState { get; set; }
        
        public bool ComparePreviousStateTypeEquality(Type stateType)
        {
            var memberInfo = PreviousState.GetType();

            if (memberInfo.IsEquivalentTo(stateType))
            {
                return true;
            }

            return false;
        }
    }
}