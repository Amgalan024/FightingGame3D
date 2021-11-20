using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Punch : AttackState
{
    public Punch(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody,  playerControls)
    {
    }
    public override void Enter()
    {
        Animator.SetBool("IsPunching",true);
        Player.IsAttacking = true;
        Player.SetDamage(Player.PunchDamage);
    }
    public override void Exit()
    {
        Animator.SetBool("IsPunching", false);
        Player.MovementSpeed = 0;
    }
    public override void Update()
    {
        
    }
    public override void FixedUpdate()
    {
        ExitAttackState();
    }
}
