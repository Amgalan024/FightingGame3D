using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

abstract public class MovementState : State
{
    public Transform EnemyTransform;
    public Transform PlayerTransform;
    protected MovementState(Player player, StateMachine stateMachine, Animator animator, Rigidbody rigidbody, PlayerControls playerControls, Transform playerTransform) : base(player, stateMachine, animator, rigidbody, playerControls)
    {
        this.PlayerTransform = playerTransform;
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
