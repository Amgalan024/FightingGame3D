using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Jump : State
{
    public int JumpCount { set; get; }
    public Jump(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody, playerControls)
    {
    }
    public override void Enter()
    {
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, Player.JumpForce, Rigidbody.velocity.z);
        JumpCount++;
    }
    public override void Exit()
    {

    }
    public override void Update()
    {
        DoubleJump();
        AttackInput();
    }
    public override void FixedUpdate()
    {
        if (Rigidbody.velocity.y < 0)
        {
            StateMachine.ChangeState(StateMachine.PlayerStates.Fall);
        }
    }
    public void DoubleJump()
    {
        if (JumpCount < 2)
        {
            JumpInput();
        }
    }
}
