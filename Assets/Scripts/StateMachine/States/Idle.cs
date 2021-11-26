using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Idle : MovementState
{
    public Idle(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls, Transform playerTransform ) : base(player, stateMachine, animator, rigidbody, playerControls, playerTransform)
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
        BlockInput();
    }
    public override void FixedUpdate()
    {
        PlayersFaceToFace();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
    }
}
