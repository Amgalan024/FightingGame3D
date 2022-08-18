using UnityEngine;

public class RunForward : MovementState
{
    public RunForward(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls, Transform playerTransform) : base(playerModel, playerStateMachineOld, animator, rigidbody,
        playerControls, playerTransform)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
        Animator.SetFloat("Forward", PlayerModel.MovementSpeed);
    }

    public override void Update()
    {
        JumpInput();
        AttackInput();
    }

    public override void FixedUpdate()
    {
        MovementHandle();
        PlayersFaceToFace();
    }

    private void MovementHandle()
    {
        if (!Input.GetKey(PlayerControls.MoveForward))
        {
            Rigidbody.velocity = new Vector3(PlayerModel.MovementSpeed, Rigidbody.velocity.y, Rigidbody.velocity.z);
            if (PlayerModel.MovementSpeed >= 0)
            {
                PlayerModel.MovementSpeed -= Time.fixedDeltaTime * 12;
            }

            if (PlayerModel.MovementSpeed <= 0)
            {
                PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Idle);
            }

            Animator.SetFloat("Forward", PlayerModel.MovementSpeed);
        }
        else
        {
            if (PlayerModel.MovementSpeed <= PlayerModel.MaxMovementSpeed)
            {
                PlayerModel.MovementSpeed += Time.fixedDeltaTime * 12;
            }

            Animator.SetFloat("Forward", PlayerModel.MovementSpeed);
            Rigidbody.velocity = new Vector3(PlayerModel.MovementSpeed * PlayerTransform.localScale.z, Rigidbody.velocity.y,
                Rigidbody.velocity.z);
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