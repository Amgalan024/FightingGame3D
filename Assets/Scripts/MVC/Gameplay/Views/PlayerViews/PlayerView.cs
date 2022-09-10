using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MVC.Configs.Animation;
using MVC.Configs.Enums;
using MVC.Gameplay.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Views
{
    public class PlayerView : MonoBehaviour
    {
        public event Action OnAttackAnimationEnded = delegate { };

        [SerializeField] private PlayerHitBoxView _hitBoxView;
        [SerializeField] private PlayerAttackHitBoxView _attackHitBoxViewView;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TweenVectorData _standingJumpTweenData;
        [SerializeField] private TweenVectorData _movingJumpTweenData;
        [SerializeField] private TweenVectorData _fallTweenData;
        [SerializeField] private Text _stateText;
        [SerializeField] private float _toMoveFloat;
        [SerializeField] private float _toMoveDuration;

        public PlayerHitBoxView HitBoxView => _hitBoxView;
        public PlayerAttackHitBoxView AttackHitBoxView => _attackHitBoxViewView;
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
                _jumpSequence.Append(transform.DOMove(transform.position + vector, _standingJumpTweenData.StepDuration))
                    .SetEase(_standingJumpTweenData.Ease)
                    .SetRelative(_standingJumpTweenData.IsRelative);
            }

            await _jumpSequence.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask MovingJumpAnimationAsync(DirectionType directionType, CancellationToken token)
        {
            _jumpSequence = DOTween.Sequence();

            var direction = (int) directionType * transform.localScale.z;

            foreach (var vector in _movingJumpTweenData.Vectors)
            {
                _jumpSequence.Append(transform.DOMove(transform.position + vector * direction,
                        _movingJumpTweenData.StepDuration))
                    .SetEase(_movingJumpTweenData.Ease)
                    .SetRelative(_movingJumpTweenData.IsRelative);
            }

            await _jumpSequence.AwaitForComplete(cancellationToken: token);
        }
    }
}