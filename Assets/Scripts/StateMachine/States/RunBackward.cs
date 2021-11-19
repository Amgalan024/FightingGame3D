using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RunBackward : State
{
    private float speed;
    public RunBackward(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody,  playerControls)
    {
    }
    public override void Enter()
    {
        speed = 0;
    }
    public override void Exit()
    {
        speed = 0;
        Animator.SetFloat("Backward", speed);
    }
    public override void Update()
    {
        JumpInput();
        AttackInput();
    }
    public override void FixedUpdate()
    {
        MovementHandle();
    }
    private void MovementHandle()
    {
        if (!Input.GetKey(PlayerControls.MoveBackward))
        {
            Rigidbody.velocity = new Vector3(0, Rigidbody.velocity.y, Rigidbody.velocity.z);
            if (speed >= 0)
            {
                speed -= Time.deltaTime * 12;
            }
            if (speed <= 0)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
            }
            Animator.SetFloat("Backward", speed);
        }
        else
        {
            if (speed <= Player.MaxMovementSpeed)
            {
                speed += Time.deltaTime * 12;
            }
            Animator.SetFloat("Backward", speed);
            Rigidbody.velocity = new Vector3(-Player.MaxMovementSpeed * PlayerTransform.localScale.z, Rigidbody.velocity.y, Rigidbody.velocity.z);
        }
       
    }
  
}
