using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Combo : AttackState
{
    public string ComboName { set; get; }
    public int ComboDamage { set; get; }

    public Combo(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.Play(ComboName);
        Animator.SetBool(ComboName,true);
        Player.IsAttacking = true;
        Player.SetDamage(ComboDamage);
    }

    public override void Exit()
    {
        Animator.SetBool(ComboName, false);
    }

    public override void FixedUpdate()
    {
        ExitAttackState();
    }

    public override void Update()
    {
    }
}

