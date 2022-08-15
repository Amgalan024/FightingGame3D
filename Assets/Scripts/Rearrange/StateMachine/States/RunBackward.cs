using UnityEngine;

public class RunBackward : MovementState
{
    public RunBackward(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls, Transform playerTransform) : base(playerModel, stateMachine, animator, rigidbody,
        playerControls, playerTransform)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
        Animator.SetFloat("Backward", PlayerModel.MovementSpeed);
    }

    public override void Update()
    {
        JumpInput();
        AttackInput();
        BlockInput();
    }

    public override void FixedUpdate()
    {
        MovementHandle();
        PlayersFaceToFace();
    }

    private void MovementHandle()
    {
        if (!Input.GetKey(PlayerControls.MoveBackward))
        {
            Rigidbody.velocity = new Vector3(PlayerModel.MovementSpeed, Rigidbody.velocity.y, Rigidbody.velocity.z);
            if (PlayerModel.MovementSpeed >= 0)
            {
                PlayerModel.MovementSpeed -= Time.fixedDeltaTime * 12;
            }

            if (PlayerModel.MovementSpeed <= 0)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
            }

            Animator.SetFloat("Backward", PlayerModel.MovementSpeed);
        }
        else
        {
            if (PlayerModel.MovementSpeed <= PlayerModel.MaxMovementSpeed)
            {
                PlayerModel.MovementSpeed += Time.fixedDeltaTime * 12;
            }

            Animator.SetFloat("Backward", PlayerModel.MovementSpeed);
            Rigidbody.velocity = new Vector3(-PlayerModel.MaxMovementSpeed * PlayerTransform.localScale.z,
                Rigidbody.velocity.y, Rigidbody.velocity.z);
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