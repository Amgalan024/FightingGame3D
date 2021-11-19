using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RunFroward : State
{
    public RunFroward(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody,  playerControls)
    {
    }
    public override void Enter()
    {
    }
    public override void Exit()
    {
        Animator.SetFloat("Forward", Player.MovementSpeed);
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
        if (!Input.GetKey(PlayerControls.MoveForward))
        {
            Rigidbody.velocity = new Vector3(Player.MovementSpeed, Rigidbody.velocity.y, Rigidbody.velocity.z);
            if (Player.MovementSpeed >= 0)
            {
                Player.MovementSpeed -= Time.deltaTime * 12;
            }
            if (Player.MovementSpeed <= 0)
            {
                StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
            }
            Animator.SetFloat("Forward", Player.MovementSpeed);
        }
        else
        {
            if (Player.MovementSpeed <= Player.MaxMovementSpeed)
            {
                Player.MovementSpeed += Time.deltaTime * 12;
            }
            Animator.SetFloat("Forward", Player.MovementSpeed);
            Rigidbody.velocity = new Vector3(Player.MovementSpeed * PlayerTransform.localScale.z, Rigidbody.velocity.y, Rigidbody.velocity.z);
        }
    }
}
