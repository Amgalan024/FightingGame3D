using UnityEngine;

public class Combo : AttackState
{
    public string Name { set; get; }
    public int Damage { set; get; }

    public Combo(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, stateMachine, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.Play(Name);
        Animator.SetBool(Name, true);
        PlayerModel.IsAttacking = true;
        PlayerModel.SetDamage(Damage);
        PlayerModel.IsDoingCombo = true;
    }

    public override void Exit()
    {
        Animator.SetBool(Name, false);
        PlayerModel.IsDoingCombo = false;
    }

    public override void FixedUpdate()
    {
        ExitAttackState();
    }

    public override void Update()
    {
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
    }
}