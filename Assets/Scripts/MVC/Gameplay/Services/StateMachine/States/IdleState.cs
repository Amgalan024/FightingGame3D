using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class IdleState : State
    {
        public IdleState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
            StateModel.InputActionModelsContainer.SetAllInputActionModels(true);
        }
    }
}