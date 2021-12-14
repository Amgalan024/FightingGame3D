using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IPlayerComponent
{
    public Player Player { private set; get; }
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        MatchingAnimatorParameters();
    }
    public void InitializeComponent(Player player)
    {
        this.Player = player;
    }
    private void MatchingAnimatorParameters()
    {
        animator.SetBool("IsAttacking", Player.IsAttacking);
        animator.SetBool("IsGrounded", Player.IsGrounded);
        animator.SetBool("IsCrouching", Player.IsCrouching);
    }
    public void SetAttackFalse()
    {
        Player.IsAttacking = false;
    }
}
