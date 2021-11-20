using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StateMachineController : MonoBehaviour, IPlayerComponent
{
    public bool isGrounded;
    public bool isAttacking;
    private StateMachine movementSM;
    private Player player;
    private PlayerStates playerStates;
    private Rigidbody playerRigidbody;
    private Animator animator;
    private PlayerControls playerControls;
    private void OnPlayerDied(int obj)
    {
        if (obj <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = player.IsGrounded;
        isAttacking = player.IsAttacking;
        movementSM.CurrentState.Update();
    }
    private void FixedUpdate()
    {
        MatchingAnimatorParameters();
        movementSM.CurrentState.FixedUpdate();
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
        player.OnHPChanged += OnPlayerDied;
        movementSM = new StateMachine();
        playerStates = new PlayerStates();
        if (player.Number == Player.PLAYER1_NUMBER)
        {
            playerControls = new PlayerControls(KeyCode.D, KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.O, KeyCode.K);
        }
        else if (player.Number == Player.PLAYER2_NUMBER)
        {
            playerControls = new PlayerControls(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.Keypad6, KeyCode.Keypad4);
        }
        playerStates.Idle = new Idle(player, movementSM, animator, playerRigidbody, playerControls, transform);
        playerStates.Crouch = new Crouch(player, movementSM, animator, playerRigidbody, playerControls, transform);
        playerStates.RunForward = new RunForward(player, movementSM, animator, playerRigidbody, playerControls, transform);
        playerStates.RunBackward = new RunBackward(player, movementSM, animator, playerRigidbody, playerControls, transform);
        playerStates.Jump = new Jump(player, movementSM, animator, playerRigidbody, playerControls, transform);
        playerStates.Fall = new Fall(player, movementSM, animator, playerRigidbody, playerControls, transform);
        playerStates.Punch = new Punch(player, movementSM, animator, playerRigidbody, playerControls);
        playerStates.Kick = new Kick(player, movementSM, animator, playerRigidbody, playerControls);
        movementSM.Initialize(playerStates.Idle, playerStates);
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
        playerStates.RunBackward.EnemyTransform = enemyTransform;
        playerStates.RunBackward.EnemyTransform = enemyTransform;
        playerStates.Jump.EnemyTransform = enemyTransform;
        playerStates.Fall.EnemyTransform = enemyTransform;
    }

}
