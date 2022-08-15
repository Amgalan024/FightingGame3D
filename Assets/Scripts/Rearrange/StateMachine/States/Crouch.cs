using UnityEngine;

public class Crouch : MovementState
{
    public Crouch(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls, Transform playerTransform) : base(playerModel, stateMachine, animator, rigidbody,
        playerControls, playerTransform)
    {
    }

    public override void Enter()
    {
        PlayerModel.IsCrouching = true;
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        AttackInput();
        BlockInput();
    }

    public override void FixedUpdate()
    {
        if (!Input.GetKey(PlayerControls.Crouch))
        {
            PlayerModel.IsCrouching = false;
            StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
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