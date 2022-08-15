using UnityEngine;

public class Kick : AttackState
{
    public Kick(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, stateMachine, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.SetBool("IsKicking", true);
        PlayerModel.IsAttacking = true;
        PlayerModel.SetDamage(PlayerModel.KickDamage);
    }

    public override void Exit()
    {
        Animator.SetBool("IsKicking", false);
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        ExitAttackState();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
    }
}