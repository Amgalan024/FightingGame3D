using UnityEngine;

public class Combo : AttackState
{
    public string Name { set; get; }
    public int Damage { set; get; }

    public Combo(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls) : base(playerModel, playerStateMachineOld, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.Play(Name);
        Animator.SetBool(Name, true);
        PlayerModel.IsAttacking.Value = true;
        //PlayerModel.SetDamage(Damage);
        PlayerModel.IsDoingCombo.Value = true;
    }

    public override void Exit()
    {
        Animator.SetBool(Name, false);
        PlayerModel.IsDoingCombo.Value = false;
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