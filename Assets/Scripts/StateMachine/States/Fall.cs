using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Fall : State
{
    public Fall(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entered FALL");
    }
    public override void Exit()
    {
    }
    public override void Update()
    {
        if (Player.IsGrounded)
        {
            StateMachine.PlayerStates.Jump.JumpCount = 0;
            StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
        }
    }
    public override void FixedUpdate()
    {
    }
}
