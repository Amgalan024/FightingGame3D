using UnityEngine;

public abstract class State
{
    public Animator Animator { private set; get; }
    public PlayerStateMachineOld PlayerStateMachineOld { private set; get; }
    public PlayerModel PlayerModel { private set; get; }
    public PlayerControls PlayerControls { private set; get; }
    public Rigidbody Rigidbody { private set; get; }

    protected State(PlayerModel playerModel, PlayerStateMachineOld playerStateMachineOld, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls)
    {
        PlayerModel = playerModel;
        PlayerStateMachineOld = playerStateMachineOld;
        Animator = animator;
        Rigidbody = rigidbody;
        PlayerControls = playerControls;
        PlayerModel.OnLose += OnLose;
        //PlayerModel.OnWin += OnWin;
        PlayerModel.OnPlayerRefreshed += OnPlayerRefreshed;
    }

    protected State(PlayerStateMachineOld playerStateMachineOld)
    {
        PlayerStateMachineOld = playerStateMachineOld;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerAttackHitBox>())
        {
            if (PlayerModel.IsBlocking)
            {
                PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Block);
            }
            else
            {
                PlayerModel.TakeDamage(collider.GetComponent<PlayerAttackHitBox>().Damage);
                Debug.Log($"Player Number {PlayerModel.Number} Took damage");
            }
        }
    }

    public virtual void OnTriggerExit(Collider collider)
    {
    }

    protected void AttackInput()
    {
        if (Input.GetKeyDown(PlayerControls.Punch))
        {
            PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Punch);
        }

        if (Input.GetKeyDown(PlayerControls.Kick))
        {
            PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Kick);
        }
    }

    protected void JumpInput()
    {
        if (Input.GetKeyDown(PlayerControls.Jump))
        {
            PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Jump);
        }
    }

    protected void MovementInput()
    {
        if (Input.GetKey(PlayerControls.MoveForward))
        {
            PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.RunForward);
        }

        if (Input.GetKey(PlayerControls.MoveBackward))
        {
            PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.RunBackward);
        }
    }

    protected void CrouchInput()
    {
        if (Input.GetKey(PlayerControls.Crouch))
        {
            PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Crouch);
        }
    }

    protected void BlockInput()
    {
        if (Input.GetKey(PlayerControls.MoveBackward))
        {
            PlayerModel.IsBlocking.Value = true;
        }
        else
        {
            PlayerModel.IsBlocking.Value = false;
        }
    }

    private void OnPlayerRefreshed()
    {
        PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Idle);
    }

    private void OnWin(int obj)
    {
        PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Death);
    }

    private void OnLose()
    {
        PlayerStateMachineOld.ChangeState(PlayerStateMachineOld.PlayerStates.Death);
    }
}