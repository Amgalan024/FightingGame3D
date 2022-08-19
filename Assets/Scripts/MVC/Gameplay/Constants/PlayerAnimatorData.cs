using UnityEngine;

namespace MVC.Gameplay.Constants
{
    public class PlayerAnimatorData
    {
        public static readonly int IsBlocking = Animator.StringToHash("IsBlocking");
        public static readonly int IsCrouching = Animator.StringToHash("IsCrouching");
        public static readonly int Forward = Animator.StringToHash("Forward");
        public static readonly int Backward = Animator.StringToHash("Backward");
        public static readonly int IsKicking = Animator.StringToHash("IsKicking");
        public static readonly int IsPunching = Animator.StringToHash("IsPunching");

    }
}