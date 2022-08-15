using UnityEngine;

public abstract class State
{
    public Animator Animator { private set; get; }
    public StateMachine StateMachine { private set; get; }
    public PlayerModel PlayerModel { private set; get; }
    public Rigidbody Rigidbody { private set; get; }
    public PlayerControls PlayerControls { private set; get; }

    protected State(PlayerModel playerModel, StateMachine stateMachine, Animator animator, Rigidbody rigidbody,
        PlayerControls playerControls)
    {
        PlayerModel = playerModel;
        StateMachine = stateMachine;
        Animator = animator;
        Rigidbody = rigidbody;
        PlayerControls = playerControls;
        PlayerModel.OnPlayerDied += OnPlayerDied;
        PlayerModel.OnPlayerWonRound += OnPlayerWonRound;
        PlayerModel.OnPlayerRefreshed += OnPlayerRefreshed;
    }

    protected State(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
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
                StateMachine.ChangeState(StateMachine.PlayerStates.Block);
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
            StateMachine.ChangeState(StateMachine.PlayerStates.Punch);
        }

        if (Input.GetKeyDown(PlayerControls.Kick))
        {
            StateMachine.ChangeState(StateMachine.PlayerStates.Kick);
        }
    }

    protected void JumpInput()
    {
        if (Input.GetKeyDown(PlayerControls.Jump))
        {
            StateMachine.ChangeState(StateMachine.PlayerStates.Jump);
        }
    }

    protected void MovementInput()
    {
        if (Input.GetKey(PlayerControls.MoveForward))
        {
            StateMachine.ChangeState(StateMachine.PlayerStates.RunForward);
        }

        if (Input.GetKey(PlayerControls.MoveBackward))
        {
            StateMachine.ChangeState(StateMachine.PlayerStates.RunBackward);
        }
    }

    protected void CrouchInput()
    {
        if (Input.GetKey(PlayerControls.Crouch))
        {
            StateMachine.ChangeState(StateMachine.PlayerStates.Crouch);
        }
    }

    protected void BlockInput()
    {
        if (Input.GetKey(PlayerControls.MoveBackward))
        {
            PlayerModel.IsBlocking = true;
        }
        else
        {
            PlayerModel.IsBlocking = false;
        }
    }

    private void OnPlayerRefreshed()
    {
        StateMachine.ChangeState(StateMachine.PlayerStates.Idle);
    }

    private void OnPlayerWonRound(int obj)
    {
        StateMachine.ChangeState(StateMachine.PlayerStates.Death);
    }

    private void OnPlayerDied()
    {
        StateMachine.ChangeState(StateMachine.PlayerStates.Death);
    }
}