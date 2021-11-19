using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Punch : State
{
    public Punch(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody,  playerControls)
    {
    }
    public override void Enter()
    {
        //Animator.Play("Cross Punch");
        Animator.SetBool("IsPunching",true);
        Player.IsAttacking = true;
        Player.SetDamage(Player.PunchDamage);
    }
    public override void Exit()
    {
        Animator.SetBool("IsPunching", false);
    }
    public override void Update()
    {
        
    }
    public override void FixedUpdate()
    {
        if (!Player.IsAttacking)
        {
            if (Player.MovementSpeed < 0.1)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
            }
            else if (Animator.GetFloat("Forward")> 0.1)
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
