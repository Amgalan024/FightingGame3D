using UnityEngine;

public class Kick : AttackState
{
    public Kick(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, playerStateMachineOld, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.SetBool("IsKicking", true);
        PlayerModel.IsAttacking.Value = true;
        //PlayerModel.SetDamage(PlayerModel.KickDamage);
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