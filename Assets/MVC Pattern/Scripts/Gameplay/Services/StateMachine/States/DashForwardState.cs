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
        public IStateMachineProxy StateMachineProxy { get; }

        private readonly RunStateModel _runStateModel;

        public DashForwardState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy,
            RunStateModel runStateModel)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
            _runStateModel = runStateModel;
        }

        public void Enter()
        {
            PlayerContainer.InputActionModelsContainer.SetAllInputActionModels(false);

            PlayerContainer.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            PlayerContainer.InputActionModelsContainer.SetJumpInputActionsFilter(true);

            PlayerContainer.View.IdleToMoveAnimationAsync(PlayerAnimatorData.Forward, Token).Forget();
        }

        public void Exit()
        {
            PlayerContainer.View.MoveToIdleAnimationAsync(PlayerAnimatorData.Forward, Token).Forget();
        }

        void IFixedTickState.OnFixedTick()
        {
            if (!Input.GetKey(_runStateModel.InputKey))
            {
                PlayerContainer.View.Rigidbody.velocity = Vector3.zero;

                StateMachineProxy.ChangeState<IdleState>();
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