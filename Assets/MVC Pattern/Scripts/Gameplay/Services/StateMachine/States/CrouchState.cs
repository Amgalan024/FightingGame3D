﻿using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class CrouchState : State, IFixedTickState
    {
        private readonly InputModelsContainer _inputs;

        public CrouchState(StateModel stateModel, PlayerView playerView, InputModelsContainer inputs) : base(stateModel,
            playerView)
        {
            _inputs = inputs;
        }

        public override void Enter()
        {
            base.Enter();

            StateModel.PlayerModel.IsCrouching.Value = true;

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);
        }

        public override void Exit()
        {
            base.Exit();

            StateModel.PlayerModel.IsCrouching.Value = false;
        }

        void IFixedTickState.OnFixedTick()
        {
            StopCrouch();
        }

        private void StopCrouch()
        {
            if (!Input.GetKey(_inputs.Crouch.Key))
            {
                StateModel.StateMachineProxy.ChangeState<IdleState>();
            }
        }
    }
}