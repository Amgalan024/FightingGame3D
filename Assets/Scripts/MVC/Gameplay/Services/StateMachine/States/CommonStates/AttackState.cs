using System;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Models;
using MVC.Views;
using UnityEditor;

namespace MVC.StateMachine.States.CommonStates
{
    public class AttackState : State
    {
        private IDisposable _exitStateSubscription;

        public AttackState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(
            stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            _exitStateSubscription = StateModel.PlayerModel.IsAttacking.Subscribe(ExitAttackState);
        }

        public override void Exit()
        {
            _exitStateSubscription.Dispose();
        }
        
        private void ExitAttackState(bool isAttacking)
        {
            if (isAttacking)
            {
                if (PlayerView.Animator.GetBool("IsCrouching"))
                {
                    StateMachineModel.ChangeState(StateModel.StatesContainer.CrouchState);
                }
                else
                {
                    if (StateModel.PlayerModel.MovementSpeed < 0.1)
                    {
                        StateMachineModel.ChangeState(StateModel.StatesContainer.IdleState);
                    }
                    else if (PlayerView.Animator.GetFloat("Forward") > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateMachineModel.ChangeState(StateModel.StatesContainer
                            .MoveForwardState);
                    }
                    else if (PlayerView.Animator.GetFloat("Backward") > 0.1)
                    {
                        StateModel.PlayerModel.MovementSpeed = 0;
                        StateMachineModel.ChangeState(StateModel.StatesContainer
                            .MoveBackwardState);
                    }
                }
            }
        }
    }
}