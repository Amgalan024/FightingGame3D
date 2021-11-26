using System;
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
        Debug.Log("Entered BlockState");
    }

    public override void Exit()
    {
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
            StateMachine.ChangeState(StateMachine.PlayerStates.RunBackward);
        }
    }

    public override void Update()
    {
    }
}
