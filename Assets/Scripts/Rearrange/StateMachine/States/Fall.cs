using UnityEngine;

public class Fall : MovementState
{
    public Fall(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls, Transform playerTransform) : base(playerModel, stateMachine, animator, rigidbody,
        playerControls, playerTransform)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        FallCheck();
        BlockInput();
    }

    private void FallCheck()
    {
        if (PlayerModel.IsGrounded)
        {
            StateMachine.PlayerStates.Jump.JumpCount = 0;
            if (PlayerModel.MovementSpeed < 0.1)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
            }
            else if (Animator.GetFloat("Forward") > 0.1)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.RunForward);
            }
            else if (Animator.GetFloat("Backward") > 0.1)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.RunBackward);
            }
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
    }
}