using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MVC.Gameplay.Constants;
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
        [SerializeField] private float _toMoveFloat;
        [SerializeField] private float _toMoveDuration;

        public PlayerHitBoxView HitBoxView => _hitBoxView;
        public PlayerAttackHitBoxView AttackHitBoxView => _attackHitBoxViewView;
        public Animator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        public Text StateText => _stateText;

        private Tween _moveAnimatorTween;

        public async UniTask IdleToMoveAnimationAsync(int moveHash, CancellationToken token)
        {
            if (_moveAnimatorTween.IsActive())
            {
                _moveAnimatorTween.Kill();
            }

            _moveAnimatorTween = DOTween.To(() => _animator.GetFloat(moveHash),
                newFloat => _animator.SetFloat(moveHash, newFloat), _toMoveFloat,
                _toMoveDuration);

            await _moveAnimatorTween.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask MoveToIdleAnimationAsync(int moveHash, CancellationToken token)
        {
            if (_moveAnimatorTween.IsActive())
            {
                _moveAnimatorTween.Kill();
            }

            _moveAnimatorTween = DOTween.To(() => _animator.GetFloat(moveHash),
                newFloat => _animator.SetFloat(moveHash, newFloat), 0, _toMoveDuration);

            await _moveAnimatorTween.AwaitForComplete(cancellationToken: token);
        }
    }
}