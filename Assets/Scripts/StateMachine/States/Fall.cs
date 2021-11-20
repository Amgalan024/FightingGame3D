using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Fall : MovementState
{
    public Fall(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls, Transform playerTransform ) : base(player, stateMachine, animator, rigidbody, playerControls, playerTransform)
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
        
    }
    public override void FixedUpdate()
    {
        FallCheck();
    }
    private void FallCheck()
    {
        if (Player.IsGrounded)
        {
            StateMachine.PlayerStates.Jump.JumpCount = 0;
            if (Player.MovementSpeed < 0.1)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
            }
            else if (Animator.GetFloat("Forward") > 0.1)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.RunForward);
            }
            else if (Animator.GetFloat("Backward") > 0.1)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.RunBackward);
            }
        }
    }
}
