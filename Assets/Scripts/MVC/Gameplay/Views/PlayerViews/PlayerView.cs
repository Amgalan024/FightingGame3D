using UnityEngine;
using UnityEngine.UI;

namespace MVC.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerHitBoxView _hitBoxView;
        [SerializeField] private PlayerAttackHitBoxView _attackHitBoxViewView;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Text _stateText;

        public PlayerHitBoxView HitBoxView => _hitBoxView;
        public PlayerAttackHitBoxView AttackHitBoxView => _attackHitBoxViewView;
        public Animator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        public Text StateText => _stateText;
    }
}