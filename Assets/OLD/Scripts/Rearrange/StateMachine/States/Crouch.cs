using UnityEngine;

public class Crouch : MovementState
{
    public Crouch(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls, Transform playerTransform) : base(playerModel, playerStateMachineOld, animator, rigidbody,
        playerControls, playerTransform)
    {
    }

    public override void Enter()
    {
        PlayerModel.IsCrouching.Value = true;
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
            PlayerModel.IsCrouching.Value = false;
            PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Idle);
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