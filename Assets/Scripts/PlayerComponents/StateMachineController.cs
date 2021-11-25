using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class StateMachineController : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private Text stateText;
    private StateMachine stateMachine;
    private Player player;
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
            stateText.text = $"{stateMachine.CurrentState.GetType()}"; 
        }
        stateMachine.CurrentState.Update();
    }
    private void FixedUpdate()
    {
        MatchingAnimatorParameters();
        stateMachine.CurrentState.FixedUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        stateMachine.CurrentState.OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        stateMachine.CurrentState.OnTriggerExit(other);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            player.IsGrounded = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            player.IsGrounded = true;
        }
    }
    public void InitializeComponent(Player player)
    {
        this.player = player;
        stateMachine = new StateMachine();
        playerStates = new PlayerStates();
        if (player.Number == Player.PLAYER1_NUMBER)
        {
            playerControls = new PlayerControls(KeyCode.D, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.O, KeyCode.K);
        }
        else if (player.Number == Player.PLAYER2_NUMBER)
        {
            playerControls = new PlayerControls(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.Keypad6, KeyCode.Keypad4);
        }
        playerStates.Idle = new Idle(player, stateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.Crouch = new Crouch(player, stateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.RunForward = new RunForward(player, stateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.RunBackward = new RunBackward(player, stateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.Jump = new Jump(player, stateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.Fall = new Fall(player, stateMachine, animator, playerRigidbody, playerControls, transform);
        playerStates.Punch = new Punch(player, stateMachine, animator, playerRigidbody, playerControls);
        playerStates.Kick = new Kick(player, stateMachine, animator, playerRigidbody, playerControls);
        playerStates.Combo = new Combo(player, stateMachine, animator, playerRigidbody, playerControls);
        playerStates.Death = new Death(player, stateMachine, animator, playerRigidbody, playerControls);
        playerStates.Block = new Block(player, stateMachine, animator, playerRigidbody, playerControls);
        stateMachine.Initialize(playerStates.Idle, playerStates);
        GetComponent<ComboHandler>().InitializeCombo(player, playerControls, stateMachine);
    }
    public Player GetPlayer()
    {
        return this.player;
    }
    public void SetAttackFalse()
    {
        player.IsAttacking = false;
    }
    private void MatchingAnimatorParameters()
    {
        animator.SetBool("IsAttacking", player.IsAttacking);
        animator.SetBool("IsGrounded", player.IsGrounded);
        animator.SetBool("IsCrouching", player.IsCrouching);
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
