using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Combo : AttackState
{
    public string Name { set; get; }
    public int Damage { set; get; }

    public Combo(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.Play(Name);
        Animator.SetBool(Name,true);
        Player.IsAttacking = true;
        Player.SetDamage(Damage);
        Player.IsDoingCombo = true;
    }

    public override void Exit()
    {
        Animator.SetBool(Name, false);
        Player.IsDoingCombo = false;
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
        throw new NotImplementedException();
    }

    public override void OnTriggerExit(Collider collider)
    {
        throw new NotImplementedException();
    }
}

