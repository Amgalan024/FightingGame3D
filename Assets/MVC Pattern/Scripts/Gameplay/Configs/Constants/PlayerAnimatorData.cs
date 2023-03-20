using UnityEngine;

namespace MVC.Gameplay.Constants
{
    public class PlayerAnimatorData
    {
        public static readonly int Win = Animator.StringToHash("Win");
        public static readonly int Lose = Animator.StringToHash("Lose");
        public static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        public static readonly int IsBlocking = Animator.StringToHash("IsBlocking");
        public static readonly int IsCrouching = Animator.StringToHash("IsCrouching");
        public static readonly int Forward = Animator.StringToHash("Forward");
        public static readonly int Backward = Animator.StringToHash("Backward");
        public static readonly int IsKicking = Animator.StringToHash("IsKicking");
        public static readonly int IsPunching = Animator.StringToHash("IsPunching");
        public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        public static readonly int IsStunned = Animator.StringToHash("IsStunned");
        public static readonly int StunnedTrigger = Animator.StringToHash("Stunned");
    }
}