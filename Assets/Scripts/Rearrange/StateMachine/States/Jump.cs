using UnityEngine;

public class Jump : MovementState
{
    public int JumpCount { set; get; }

    public Jump(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls, Transform playerTransform) : base(playerModel, stateMachine, animator, rigidbody,
        playerControls, playerTransform)
    {
    }

    public override void Enter()
    {
        Debug.Log($"{StateMachine.PlayerStates.Punch.GetType().BaseType}");
        if (!PreviousStateEqualsAttackState())
        {
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, PlayerModel.JumpForce, Rigidbody.velocity.z);
            JumpCount++;
        }
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        DoubleJump();
        AttackInput();
        BlockInput();
    }

    public override void FixedUpdate()
    {
        if (Rigidbody.velocity.y <= 0)
        {
            StateMachine.ChangeState(StateMachine.PlayerStates.Fall);
        }
    }

    public void DoubleJump()
    {
        if (JumpCount < 2)
        {
            JumpInput();
        }
    }

    public bool PreviousStateEqualsAttackState()
    {
        if (StateMachine.PreviousState.GetType().BaseType
            .IsEquivalentTo(StateMachine.PlayerStates.Punch.GetType().BaseType))
        {
            return true;
        }

        return false;
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
    }
}