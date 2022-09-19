using MVC.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachineController : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Text stateText;
    public PlayerStateMachineOld PlayerStateMachineOld { private set; get; }
    public PlayerModel PlayerModel { private set; get; }

    private PlayerStates playerStates;
    private Rigidbody playerRigidbody;
    private Animator animator;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (stateText)
        {
            stateText.text = $"{PlayerStateMachineOld.CurrentState.GetType()}";
        }

        PlayerStateMachineOld.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        PlayerStateMachineOld.CurrentState.FixedUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStateMachineOld.CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerStateMachineOld.CurrentState.OnTriggerExit(other);
    }

    public void InitializeComponent(PlayerModel playerModel)
    {
        PlayerModel = playerModel;
        PlayerStateMachineOld = new PlayerStateMachineOld();
        playerStates = new PlayerStates();

        if (playerModel.Number == PlayerModel.PLAYER1_NUMBER)
        {
            playerControls = new PlayerControls(KeyCode.D, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.O, KeyCode.K);
        }
        else if (playerModel.Number == PlayerModel.PLAYER2_NUMBER)
        {
            playerControls = new PlayerControls(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow,
                KeyCode.DownArrow, KeyCode.Keypad6, KeyCode.Keypad4);
        }

        playerStates.Idle = new Idle(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls, transform);
        playerStates.Crouch =
            new Crouch(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls, transform);
        playerStates.RunForward =
            new RunForward(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls, transform);
        playerStates.RunBackward =
            new RunBackward(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls, transform);
        playerStates.Jump = new Jump(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls, transform);
        playerStates.Fall = new Fall(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls, transform);
        playerStates.Block = new Block(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls);
        playerStates.Punch = new Punch(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls);
        playerStates.Kick = new Kick(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls);
        playerStates.Combo = new Combo(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls);
        playerStates.Death = new Death(playerModel, PlayerStateMachineOld, animator, playerRigidbody, playerControls);
        PlayerStateMachineOld.Initialize(playerStates.Idle, playerStates);
        //GetComponent<ComboInputController>().InitializeCombo(playerModel, playerControls, PlayerStateMachineOld);
    }

    public void InitializeEnemyForPlayer(Transform enemyTransform)
    {
        playerStates.Idle.EnemyTransform = enemyTransform;
        playerStates.Crouch.EnemyTransform = enemyTransform;
        playerStates.RunForward.EnemyTransform = enemyTransform;
        playerStates.RunBackward.EnemyTransform = enemyTransform;
        playerStates.Jump.EnemyTransform = enemyTransform;
        playerStates.Fall.EnemyTransform = enemyTransform;
    }
}