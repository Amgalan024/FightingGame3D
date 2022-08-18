using MVC.Gameplay.Models;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class WinState : State
    {
        public WinState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        
    }
}