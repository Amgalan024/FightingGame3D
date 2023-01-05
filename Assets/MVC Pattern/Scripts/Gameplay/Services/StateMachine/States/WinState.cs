using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public class WinState : IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        public WinState(PlayerContainer playerContainer)
        {
            PlayerContainer = playerContainer;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}