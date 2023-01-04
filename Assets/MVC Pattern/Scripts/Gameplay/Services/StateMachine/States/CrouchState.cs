using MVC.Gameplay.Models.Player;
using MVC.Gameplay.Models.StateMachineModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class CrouchState : IPlayerState, IFixedTickState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachineProxy StateMachineProxy { get; }

        public CrouchState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
        }

        public void Enter()
        {
            PlayerContainer.Model.IsCrouching.Value = true;

            PlayerContainer.InputActionModelsContainer.SetAllInputActionModels(false);

            PlayerContainer.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            PlayerContainer.InputActionModelsContainer.SetBlockInputActionsFilter(true);
        }

        public void Exit()
        {
            PlayerContainer.Model.IsCrouching.Value = false;
        }

        void IFixedTickState.OnFixedTick()
        {
            StopCrouch();
        }

        private void StopCrouch()
        {
            if (!Input.GetKey(PlayerContainer.InputModelsContainer.Crouch.Key))
            {
                StateMachineProxy.ChangeState<IdleState>();
            }
        }
    }
}