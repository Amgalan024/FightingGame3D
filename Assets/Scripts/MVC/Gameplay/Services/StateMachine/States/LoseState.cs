using MVC.Gameplay.Models;
using MVC.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class LoseState : State
    {
        public LoseState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}