using System;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

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

            _fallSubscription = StateModel.PlayerModel.IsGrounded.Subscribe(OnFall);

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);
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
                StateModel.PlayerModel.CurrentJumpCount = 0;

                if (StateModel.PlayerModel.MovementSpeed < 0.1)
                {
                    StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
                }
            }
        }
    }
}