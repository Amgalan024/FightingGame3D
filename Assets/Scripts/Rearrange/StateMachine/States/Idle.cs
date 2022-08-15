using UnityEngine;

public class Idle : MovementState
{
    public Idle(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
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
        MovementInput();
        CrouchInput();
        JumpInput();
        AttackInput();
        BlockInput();
    }

    public override void FixedUpdate()
    {
        PlayersFaceToFace();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
    }
}