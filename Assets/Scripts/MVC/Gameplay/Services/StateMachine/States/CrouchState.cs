using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Models;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.StateMachine.States
{
    public class CrouchState : State, ITickable
    {
        private readonly InputModelsContainer _inputs;

        public CrouchState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage,
            InputModelsContainer inputs) : base(stateModel, playerView, storage)
        {
            _inputs = inputs;
        }

        public override void Enter()
        {
            StateModel.PlayerModel.IsCrouching.Value = true;

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
            StateModel.PlayerModel.IsCrouching.Value = false;
        }

        private void StopCrouch()
        {
            if (!Input.GetKey(_inputs.Crouch.Key))
            {
                StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
            }
        }
    }
}