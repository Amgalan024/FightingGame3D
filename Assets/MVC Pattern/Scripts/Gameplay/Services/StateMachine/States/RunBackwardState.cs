using Cysharp.Threading.Tasks;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class RunBackwardState : State
    {
        public RunBackwardState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(
            stateModel, playerView, storage)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetJumpInputActionsFilter(true);

            PlayerView.IdleToMoveAnimationAsync(PlayerAnimatorData.Backward, Token).Forget();
        }

        public override void OnFixedTick()
        {
            if (!Input.GetKey(StateModel.InputModelsContainer.MoveBackward.Key))
            {
                PlayerView.Rigidbody.velocity = Vector3.zero;

                StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
            }
            else
            {
                var velocity = PlayerView.Rigidbody.velocity;

                velocity.x = -StateModel.PlayerModel.MaxMovementSpeed * PlayerView.transform.localScale.z;

                PlayerView.Rigidbody.velocity = velocity;
            }
        }

        public override void Exit()
        {
            base.Exit();

            PlayerView.MoveToIdleAnimationAsync(PlayerAnimatorData.Backward, Token).Forget();
        }
    }
}