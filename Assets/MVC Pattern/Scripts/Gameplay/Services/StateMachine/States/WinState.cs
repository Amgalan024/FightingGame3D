using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public class WinState : IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachineProxy StateMachineProxy { get; }

        public WinState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}