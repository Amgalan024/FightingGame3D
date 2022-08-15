using UnityEngine;

abstract public class AttackState : State
{
    protected AttackState(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, stateMachine, animator, rigidbody, playerControls)
    {
    }

    public void ExitAttackState()
    {
        if (!PlayerModel.IsAttacking)
        {
            if (Animator.GetBool("IsCrouching"))
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Crouch);
            }
            else
            {
                if (PlayerModel.MovementSpeed < 0.1)
                {
                    StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
                }
                else if (Animator.GetFloat("Forward") > 0.1)
                {
                    PlayerModel.MovementSpeed = 0;
                    StateMachine.ChangeState(StateMachine.PlayerStates.RunForward);
                }
                else if (Animator.GetFloat("Backward") > 0.1)
                {
                    PlayerModel.MovementSpeed = 0;
                    StateMachine.ChangeState(StateMachine.PlayerStates.RunBackward);
                }
            }
        }
    }
}