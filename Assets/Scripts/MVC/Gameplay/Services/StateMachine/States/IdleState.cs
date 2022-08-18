using MVC.Gameplay.Models;
using MVC.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class IdleState : State
    {
        public IdleState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            StateModel.InputActionModelsContainer.SetAllInputActionModels(true);
            SetFaceToFace();
        }

        private void SetFaceToFace()
        {
        }
    }
}