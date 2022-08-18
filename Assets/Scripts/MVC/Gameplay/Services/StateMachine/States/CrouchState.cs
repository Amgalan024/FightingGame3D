using MVC.Gameplay.Models;
using MVC.Models;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.StateMachine.States
{
    public class CrouchState : State, ITickable
    {
        private readonly ControlModelsContainer _controls;

        public CrouchState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView, ControlModelsContainer controls) : base(stateModel, stateMachineModel, playerView)
        {
            _controls = controls;
        }

        public override void Enter()
        {
            StateModel.PlayerModel.IsCrouching.Value  = true;

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);
        }

        public void Tick()
        {
            StopCrouch();
        }

        public override void Exit()
        {
            StateModel.PlayerModel.IsCrouching.Value  = false;
        }

        private void StopCrouch()
        {
            if (!Input.GetKey(_controls.Crouch.Key))
            {
                StateMachineModel.ChangeState(StateModel.StatesContainer.IdleState);
            }
        }

    
    }
}