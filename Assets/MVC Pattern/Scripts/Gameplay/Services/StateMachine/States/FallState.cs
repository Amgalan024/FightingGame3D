using System;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class FallState : State
    {
        private IDisposable _fallSubscription;

        public FallState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);

            _fallSubscription = StateModel.PlayerModel.IsGrounded.Subscribe(OnFall);
        }

        public override void Exit()
        {
            base.Exit();

            _fallSubscription?.Dispose();
        }

        private void OnFall(bool isGrounded)
        {
            if (isGrounded)
            {
                Debug.Log("CurrentJumpCount = 0");
                StateModel.PlayerModel.CurrentJumpCount = 0;

                StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
            }
        }
    }
}