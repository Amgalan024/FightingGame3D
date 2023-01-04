using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public class DashBackwardState : IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachineProxy StateMachineProxy { get; }

        public void Enter()
        {
            StateMachineProxy.ChangeState<IdleState>();
        }

        public void Exit()
        {
        }
    }
}