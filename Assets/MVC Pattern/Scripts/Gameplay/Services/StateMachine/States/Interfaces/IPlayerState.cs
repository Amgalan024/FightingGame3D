using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public interface IPlayerState : IState
    {
        PlayerContainer PlayerContainer { get; }
    }
}