using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RunBackward : MovementState
{
    public RunBackward(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls, Transform playerTransform ) : base(player, stateMachine, animator, rigidbody, playerControls, playerTransform)
    {
    }
    public override void Enter()
    {
    }
    public override void Exit()
    {
        Animator.SetFloat("Backward", Player.MovementSpeed);
    }
    public override void Update()
    {
        JumpInput();
        AttackInput();
    }
    public override void FixedUpdate()
    {
        MovementHandle();
        PlayersFaceToFace();
    }
    private void MovementHandle()
    {
        if (!Input.GetKey(PlayerControls.MoveBackward))
        {
            Rigidbody.velocity = new Vector3(Player.MovementSpeed, Rigidbody.velocity.y, Rigidbody.velocity.z);
            if (Player.MovementSpeed >= 0)
            {
                Player.MovementSpeed -= Time.fixedDeltaTime * 12;
            }
            if (Player.MovementSpeed <= 0)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
            }
            Animator.SetFloat("Backward", Player.MovementSpeed);
        }
        else
        {
            if (Player.MovementSpeed <= Player.MaxMovementSpeed)
            {
                Player.MovementSpeed += Time.fixedDeltaTime * 12;
            }
            Animator.SetFloat("Backward", Player.MovementSpeed);
            Rigidbody.velocity = new Vector3(-Player.MaxMovementSpeed * PlayerTransform.localScale.z, Rigidbody.velocity.y, Rigidbody.velocity.z);
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
