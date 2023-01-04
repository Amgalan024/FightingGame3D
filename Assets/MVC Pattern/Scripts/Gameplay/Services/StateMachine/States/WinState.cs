using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class WinState : State
    {
        public WinState(StateModel stateModel, PlayerView playerView) : base(stateModel, playerView)
        {
        }
    }
}