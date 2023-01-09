using MVC.Gameplay.Models.Player;

namespace MVC.StateMachine.States
{
    public interface IPlayerState : IState
    {
        PlayerContainer PlayerContainer { get; }
    }
}