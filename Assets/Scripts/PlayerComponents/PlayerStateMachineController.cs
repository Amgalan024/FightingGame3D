using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStateMachineController : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Text stateText;
    public StateMachine StateMachine { private set; get; }

    public Player Player { set; get; }
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
            stateText.text = $"{StateMachine.CurrentState.GetType()}"; 
        }
        StateMachine.CurrentState.Update();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        StateMachine.CurrentState.OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        StateMachine.CurrentState.OnTriggerExit(other);
    }
    public void InitializeComponent(Player player)
    {
        this.Player = player;
        StateMachine = new StateMachine();
        playerStates = new PlayerStates();
        if (player.Number == Player.PLAYER1_NUMBER)
        {
            playerControls = new PlayerControls(KeyCode.D, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.O, KeyCode.K);
        }
        else if (player.Number == Player.PLAYER2_NUMBER)
        {
            playerControls = new PlayerControls(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.Keypad6, KeyCode.Keypad4);
        }
        playerStates.Idle = new Idle(player, StateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.Crouch = new Crouch(player, StateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.RunForward = new RunForward(player, StateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.RunBackward = new RunBackward(player, StateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.Jump = new Jump(player, StateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.Fall = new Fall(player, StateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.Punch = new Punch(player, StateMachine, animator, playerRigidbody, playerControls);
        playerStates.Kick = new Kick(player, StateMachine, animator, playerRigidbody, playerControls);
        playerStates.Combo = new Combo(player, StateMachine, animator, playerRigidbody, playerControls);
        playerStates.Death = new Death(player, StateMachine, animator, playerRigidbody, playerControls);
        playerStates.Block = new Block(player, StateMachine, animator, playerRigidbody, playerControls);
        StateMachine.Initialize(playerStates.Idle, playerStates);
        GetComponent<ComboHandler>().InitializeCombo(player, playerControls, StateMachine);
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
