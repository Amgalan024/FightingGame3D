namespace MVC_Pattern.Scripts.Gameplay.Services.StateMachine
{
    public interface IStateMachineProxy : IStateMachine
    {
        void SetStateMachine(IStateMachine stateMachine);
    }
}