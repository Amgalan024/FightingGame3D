using UnityEngine;

public class Death : State
{
    public Death(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, playerStateMachineOld, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider collider)
    {
    }

    public override void OnTriggerExit(Collider collider)
    {
    }

    public override void Update()
    {
    }
}