using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MVC.Configs.Animation;
using MVC.Configs.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Views
{
    public class PlayerView : MonoBehaviour
    {
        public event Action OnAttackAnimationEnded = delegate { };

        [SerializeField] private TriggerDetectorView _triggerDetector;
        [SerializeField] private CollisionDetectorView _collisionDetector;
        [SerializeField] private PlayerAttackHitBoxView _attackHitBoxViewView;
        [SerializeField] private PlayerSideDetectorView _sideDetectorView;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TweenVectorData _standingJumpTweenData;
        [SerializeField] private TweenVectorData _movingJumpTweenData;
        [SerializeField] private TweenVectorData _fallTweenData;
        [SerializeField] private Text _stateText;
        [SerializeField] private float _toMoveFloat;
        [SerializeField] private float _toMoveDuration;

        public TriggerDetectorView TriggerDetector => _triggerDetector;
        public CollisionDetectorView CollisionDetector => _collisionDetector;
        public PlayerAttackHitBoxView AttackHitBoxView => _attackHitBoxViewView;
        public PlayerSideDetectorView SideDetectorView => _sideDetectorView;
        public Animator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        public Text StateText => _stateText;

        private Tween _idleToMoveTween;

        private Tween _moveToIdleTween;

        private Sequence _jumpSequence;

        public void InvokeAttackAnimationEnd()
        {
            OnAttackAnimationEnded.Invoke();
        }

        public async UniTask IdleToMoveAnimationAsync(int moveHash, CancellationToken token)
        {
            await UniTask.WaitUntil(() => !_moveToIdleTween.IsActive(), cancellationToken: token);

            _idleToMoveTween = DOTween.To(() => _animator.GetFloat(moveHash),
                newFloat => _animator.SetFloat(moveHash, newFloat), _toMoveFloat,
                _toMoveDuration);

            await _idleToMoveTween.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask MoveToIdleAnimationAsync(int moveHash, CancellationToken token)
        {
            _moveToIdleTween = DOTween.To(() => _animator.GetFloat(moveHash),
                newFloat => _animator.SetFloat(moveHash, newFloat), 0, _toMoveDuration);

            await _moveToIdleTween.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask StandingJumpAnimationAsync(CancellationToken token)
        {
            _jumpSequence = DOTween.Sequence();

            foreach (var vector in _standingJumpTweenData.Vectors)
            {
                DOTween.To(() => _rigidbody.velocity,
                        newValue => _rigidbody.velocity = newValue, vector, _toMoveDuration)
                    .SetEase(_standingJumpTweenData.Ease);
            }

            await _jumpSequence.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask MovingJumpAnimationAsync(DirectionType directionType, CancellationToken token)
        {
            _jumpSequence = DOTween.Sequence();

            var direction = (int) directionType * transform.localScale.z;

            foreach (var vector in _movingJumpTweenData.Vectors)
            {
                DOTween.To(() => _rigidbody.velocity,
                        newValue => _rigidbody.velocity = newValue, vector * direction, _toMoveDuration)
                    .SetEase(_standingJumpTweenData.Ease);
            }

            await _jumpSequence.AwaitForComplete(cancellationToken: token);
        }
    }
}