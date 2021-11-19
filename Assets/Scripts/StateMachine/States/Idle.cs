using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Idle : State
{
    public Idle(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody, playerControls)
    {
    }
    public override void Enter()
    {
    }
    public override void Exit()
    {
    }
    public override void Update()
    {
        MovementInput();
        CrouchInput();
        JumpInput();
        AttackInput();
    }
    public override void FixedUpdate()
    {
    }
}
