using System;

namespace MVC.Gameplay.Models.StateMachineModels
{
    public class StateMachineProxy
    {
        public event Action<Type> OnStateChanged;

        public void ChangeState(Type stateType)
        {
            OnStateChanged?.Invoke(stateType);
        }
    }
}