using Cysharp.Threading.Tasks;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC.Utils.Disposable;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class DashForwardState : DisposableWithCts, IPlayerState, IFixedTickState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        private readonly RunStateModel _runStateModel;

        public DashForwardState(PlayerContainer playerContainer, RunStateModel runStateModel)
        {
            PlayerContainer = playerContainer;
            _runStateModel = runStateModel;
        }

        public void Enter()
        {
            ConfigureInputActionsFilters();

            PlayerContainer.View.PlayIdleToMoveAnimationAsync(PlayerAnimatorData.Forward, Token).Forget();
        }

        public void Exit()
        {
            PlayerContainer.View.PlayMoveToIdleAnimationAsync(PlayerAnimatorData.Forward, Token).Forget();
        }

        void IFixedTickState.OnFixedTick()
        {
            HandleDashKeyInput();
        }

        private void ConfigureInputActionsFilters()
        {
            PlayerContainer.InputModelsContainer.SetAllInputActionModelFilters(false);

            PlayerContainer.InputModelsContainer.SetAttackInputActionFilters(true);
            PlayerContainer.InputModelsContainer.SetJumpInputActionFilter(true);
        }

        private void HandleDashKeyInput()
        {
            if (!Input.GetKey(_runStateModel.InputKey))
            {
                PlayerContainer.View.Rigidbody.velocity = Vector3.zero;

                StateMachine.ChangeState<IdleState>();
            }
            else
            {
                var velocity = PlayerContainer.View.Rigidbody.velocity;

                velocity.x = PlayerContainer.Model.MaxMovementSpeed * 2 *
                             PlayerContainer.View.GetPlayerDirection();

                PlayerContainer.View.Rigidbody.velocity = velocity;
            }
        }
    }
}