using UnityEngine;

public class Death : State
{
    public Death(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, stateMachine, animator, rigidbody, playerControls)
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