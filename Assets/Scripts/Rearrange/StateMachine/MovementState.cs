using UnityEngine;

abstract public class MovementState : State
{
    public Transform EnemyTransform;
    public Transform PlayerTransform;

    protected MovementState(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls, Transform playerTransform) : base(playerModel, playerStateMachineOld, animator, rigidbody,
        playerControls)
    {
        PlayerTransform = playerTransform;
    }

    public void PlayersFaceToFace()
    {
        if (PlayerModel.AtLeftSide && PlayerTransform.position.x > EnemyTransform.position.x)
        {
            SwitchMovementControls();
            PlayerModel.AtRightSide = true;
            PlayerModel.AtLeftSide = false;
            PlayerTransform.localScale = new Vector3(PlayerTransform.localScale.x, PlayerTransform.localScale.y, -1);
        }

        if (PlayerModel.AtRightSide && PlayerTransform.position.x < EnemyTransform.position.x)
        {
            SwitchMovementControls();
            PlayerModel.AtLeftSide = true;
            PlayerModel.AtRightSide = false;
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