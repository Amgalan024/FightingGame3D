﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Block : State
{
    public Block(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody, playerControls)
    {
    }

    public override void Enter()
    {
        Animator.SetBool("IsBlocking", true);
        Debug.Log("Entered BlockState");
    }

    public override void Exit()
    {
        Animator.SetBool("IsBlocking", false);
        Debug.Log("Exited BlockState");
    }

    public override void FixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider collider)
    {
        
    }

    public override void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<PlayerAttackHitBox>())
        {
            if (Animator.GetBool("IsCrouching"))
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Crouch);
            }
            else
            {
                if (Player.MovementSpeed < 0.1)
                {
                    StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
                }
                else if (Animator.GetFloat("Forward") > 0.1)
                {
                    Player.MovementSpeed = 0;
                    StateMachine.ChangeState(StateMachine.PlayerStates.RunForward);
                }
                else if (Animator.GetFloat("Backward") > 0.1)
                {
                    Player.MovementSpeed = 0;
                    StateMachine.ChangeState(StateMachine.PlayerStates.RunBackward);
                }
            }
        }
    }

    public override void Update()
    {
    }
}
