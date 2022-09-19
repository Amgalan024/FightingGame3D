using UnityEngine;

public class Block : State
{
    public Block(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, playerStateMachineOld, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.SetBool("IsBlocking", true);
        Debug.Log("Entered BlockState");
    }

    public override void Exit()
    {
        Animator.SetBool("IsBlocking", false);
        Debug.Log("Exited BlockState");
    }

    public override void FixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider collider)
    {
    }

    public override void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<PlayerAttackHitBox>())
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

    public override void Update()
    {
    }
}