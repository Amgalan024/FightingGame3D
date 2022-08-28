using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class RunForwardState : State
    {
        public RunForwardState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(
            stateModel, playerView, storage)
        {
        }

        public override void Enter()
        {
            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetJumpInputActionsFilter(true);
        }

        public override void OnFixedTick()
        {
            if (!Input.GetKey(StateModel.InputModelsContainer.MoveForward.Key))
            {
                var velocity = PlayerView.Rigidbody.velocity;

                velocity.x = StateModel.PlayerModel.MovementSpeed;

                PlayerView.Rigidbody.velocity = velocity;

                if (StateModel.PlayerModel.MovementSpeed >= 0)
                {
                    StateModel.PlayerModel.MovementSpeed -= Time.fixedDeltaTime * 12;
                }

                if (StateModel.PlayerModel.MovementSpeed <= 0)
                {
                    StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
                }

                PlayerView.Animator.SetFloat(PlayerAnimatorData.Forward, StateModel.PlayerModel.MovementSpeed);
            }
            else
            {
                if (StateModel.PlayerModel.MovementSpeed <= StateModel.PlayerModel.MaxMovementSpeed)
                {
                    StateModel.PlayerModel.MovementSpeed += Time.fixedDeltaTime * 12;
                }

                PlayerView.Animator.SetFloat(PlayerAnimatorData.Forward, StateModel.PlayerModel.MovementSpeed);

                var velocity = PlayerView.Rigidbody.velocity;

                velocity.x = StateModel.PlayerModel.MovementSpeed * PlayerView.transform.localScale.z;

                PlayerView.Rigidbody.velocity = velocity;
            }
        }

        public override void Exit()
        {
            PlayerView.Animator.SetFloat(PlayerAnimatorData.Forward, StateModel.PlayerModel.MovementSpeed);
        }
    }
}