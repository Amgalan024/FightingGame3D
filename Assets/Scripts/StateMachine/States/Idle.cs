using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Idle : State
{
    public Transform EnemyTransform { set; get; }
    private float playerLocalScaleX;

    public Idle(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls) : base(player, stateMachine, animator, rigidbody, playerControls)
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
    }
    public override void FixedUpdate()
    {
        PlayersFaceToFace();
    }
    public void PlayersFaceToFace()
    {
        if (Player.AtLeftSide && PlayerTransform.position.x > EnemyTransform.position.x)
        {
            SwitchMovementControls();
            Player.AtRightSide = true;
            Player.AtLeftSide = false;
            PlayerTransform.localScale = new Vector3(PlayerTransform.localScale.x, PlayerTransform.localScale.y, -1);
        }
        if (Player.AtRightSide && PlayerTransform.position.x < EnemyTransform.position.x)
        {
            SwitchMovementControls();
            Player.AtLeftSide = true;
            Player.AtRightSide = false;
            PlayerTransform.localScale = new Vector3(PlayerTransform.localScale.x, PlayerTransform.localScale.y, 1);
        }
    }
    public void SwitchMovementControls()
    {
        KeyCode tempMoveForward = PlayerControls.MoveForward;
        KeyCode tempMoveBackward = PlayerControls.MoveBackward;
        PlayerControls.MoveForward = tempMoveBackward;
        PlayerControls.MoveBackward = tempMoveForward;
    }
}
