using UnityEngine;

public class Punch : AttackState
{
    public Punch(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, stateMachine, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.SetBool("IsPunching", true);
        PlayerModel.IsAttacking = true;
        PlayerModel.SetDamage(PlayerModel.PunchDamage);
    }

    public override void Exit()
    {
        Animator.SetBool("IsPunching", false);
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