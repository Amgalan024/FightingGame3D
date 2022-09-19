using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IPlayerComponent
{
    public PlayerModel PlayerModel { private set; get; }
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MatchingAnimatorParameters();
    }

    public void InitializeComponent(PlayerModel playerModel)
    {
        PlayerModel = playerModel;
    }

    private void MatchingAnimatorParameters()
    {
        animator.SetBool("IsAttacking", PlayerModel.IsAttacking);
        animator.SetBool("IsGrounded", PlayerModel.IsGrounded);
        animator.SetBool("IsCrouching", PlayerModel.IsCrouching);
    }

    public void SetAttackFalse()
    {
        PlayerModel.IsAttacking.Value = false;
    }
}