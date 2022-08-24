using MVC.StateMachine.States;

namespace MVC.Gameplay.Models
{
    public class StateMachineModel
    {
        public IState CurrentState { get; set; }
        public IState PreviousState { get; set; }
    }
}