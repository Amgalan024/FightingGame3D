using MVC.Gameplay.Models;
using MVC.Models;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.StateMachine.States
{
    public class ComboState : State, ITickable
    {
        public string Name { set; get; }
        public int Damage { set; get; }

        public ComboState(StateModel stateModel, StateMachineModel stateMachineModel, PlayerView playerView) : base(stateModel, stateMachineModel, playerView)
        {
        }

        public override void Enter()
        {
            PlayerView.Animator.Play(Name);
            PlayerView.Animator.SetBool(Name, true);
            StateModel.PlayerModel.IsAttacking.Value = true;
            StateModel.PlayerAttackModel.Damage = Damage;
            StateModel.PlayerModel.IsDoingCombo.Value  = true;
        }

        public void Tick()
        {
            ExitAttackState();
        }

        public override void Exit()
        {
            PlayerView.Animator.SetBool(Name, false);
            StateModel.PlayerModel.IsDoingCombo.Value  = false;
        }

        public void ExitAttackState()
        {
            if (!StateModel.PlayerModel.IsAttacking)
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