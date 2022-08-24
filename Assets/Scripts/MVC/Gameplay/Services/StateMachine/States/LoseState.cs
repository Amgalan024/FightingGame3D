using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class LoseState : State
    {
        public LoseState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}