﻿using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

namespace MVC.StateMachine.States
{
    public class DashBackwardState : State
    {
        public DashBackwardState(StateModel stateModel, PlayerView playerView) : base(stateModel, playerView)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            StateModel.StateMachineProxy.ChangeState<IdleState>();
        }
    }
}