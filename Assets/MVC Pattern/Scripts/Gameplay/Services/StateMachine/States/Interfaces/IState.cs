using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public interface IState
    {
        IStateMachine StateMachine { get; set; }
        void Enter();
        void Exit();
    }
}