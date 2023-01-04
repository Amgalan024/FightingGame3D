using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.Gameplay.Models.StateMachineModels
{
    public class StateMachineProxy : IStateMachineProxy
    {
        private IStateMachine _stateMachine;

        public void SetStateMachine(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void ChangeState<T>() where T : IState
        {
            _stateMachine.ChangeState<T>();
        }
    }
}