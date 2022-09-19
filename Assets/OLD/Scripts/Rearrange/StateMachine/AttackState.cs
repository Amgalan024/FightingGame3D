using UnityEngine;

abstract public class AttackState : State
{
    protected AttackState(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, playerStateMachineOld, animator, rigidbody, playerControls)
    {
    }

    public void ExitAttackState()
    {
        if (!PlayerModel.IsAttacking)
        {
            if (Animator.GetBool("IsCrouching"))
            {
                PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Crouch);
            }
            else
            {
                if (PlayerModel.MovementSpeed < 0.1)
                {
                    PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Idle);
                }
                else if (Animator.GetFloat("Forward") > 0.1)
                {
                    PlayerModel.MovementSpeed = 0;
                    PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.RunForward);
                }
                else if (Animator.GetFloat("Backward") > 0.1)
                {
                    PlayerModel.MovementSpeed = 0;
                    PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.RunBackward);
                }
            }
        }
    }
}