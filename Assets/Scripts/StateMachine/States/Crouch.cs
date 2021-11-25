using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Crouch : MovementState
{
    public Crouch(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls, Transform playerTransform) : base(player, stateMachine, animator, rigidbody, playerControls, playerTransform)
    {
    }

    public override void Enter()
    {
        Player.IsCrouching = true;
    }
    public override void Exit()
    {
    }
    public override void Update()
    {
        AttackInput();
    }
    public override void FixedUpdate()
    {
        if (!Input.GetKey(PlayerControls.Crouch))
        {
            Player.IsCrouching = false;
            StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
        }
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
