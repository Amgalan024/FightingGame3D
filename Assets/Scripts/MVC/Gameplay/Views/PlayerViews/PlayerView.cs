using UnityEngine;

namespace MVC.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerHitBoxView _hitBoxView;
        [SerializeField] private PlayerAttackHitBoxView _attackHitBoxViewView;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;

        public PlayerHitBoxView HitBoxView => _hitBoxView;
        public PlayerAttackHitBoxView AttackHitBoxView => _attackHitBoxViewView;
        public Animator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
    }
}