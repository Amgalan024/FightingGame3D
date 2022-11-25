using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MVC.Configs.Animation;
using UnityEngine;

namespace MVC.Views
{
    public class PlayerView : MonoBehaviour
    {
        public event Action OnAttackAnimationEnded = delegate { };

        [SerializeField] private PlayerTriggerDetectorView _playerTriggerDetector;
        [SerializeField] private CollisionDetectorView _collisionDetector;
        [SerializeField] private PlayerAttackHitBoxView _attackHitBoxViewView;
        [SerializeField] private PlayerSideDetectorView _sideDetectorView;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _toMoveFloat;
        [SerializeField] private float _toMoveDuration;
        [SerializeField] private float _knockBackOnFall;
        [SerializeField] private float _knockBackOnFallDuration = 0.2f;

        public PlayerTriggerDetectorView PlayerTriggerDetector => _playerTriggerDetector;
        public CollisionDetectorView CollisionDetector => _collisionDetector;
        public PlayerAttackHitBoxView AttackHitBoxView => _attackHitBoxViewView;
        public PlayerSideDetectorView SideDetectorView => _sideDetectorView;
        public Animator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;

        public Tween IdleToMoveTween { get; private set; }
        public Tween MoveToIdleTween { get; private set; }
        public Sequence JumpSequence { get; private set; }
        public Sequence FallSequence { get; private set; }

        public Tween KnockBackTween { get; private set; }

        public void InvokeAttackAnimationEnd()
        {
            OnAttackAnimationEnded.Invoke();
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public async UniTask DashForward()
        {
        }

        public async UniTask DashBackward()
        {
        }

        public int GetPlayerDirection()
        {
            return (int) transform.localScale.z;
        }

        public async UniTask IdleToMoveAnimationAsync(int moveHash, CancellationToken token)
        {
            await UniTask.WaitUntil(() => !MoveToIdleTween.IsActive(), cancellationToken: token);

            IdleToMoveTween = DOTween.To(() => _animator.GetFloat(moveHash),
                newFloat => _animator.SetFloat(moveHash, newFloat), _toMoveFloat,
                _toMoveDuration);

            await IdleToMoveTween.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask MoveToIdleAnimationAsync(int moveHash, CancellationToken token)
        {
            MoveToIdleTween = DOTween.To(() => _animator.GetFloat(moveHash),
                newFloat => _animator.SetFloat(moveHash, newFloat), 0, _toMoveDuration);

            await MoveToIdleTween.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask JumpAnimationAsync(TweenVectorData tweenVectorData, int direction,
            CancellationToken token)
        {
            if (JumpSequence.IsActive())
            {
                JumpSequence?.Kill();
            }

            JumpSequence = DOTween.Sequence();

            foreach (var vector in tweenVectorData.Vectors)
            {
                var newVector = vector;

                newVector.x *= direction;

                JumpSequence.Append(DOTween.To(() => _rigidbody.velocity,
                        newValue => _rigidbody.velocity = newValue, newVector, _toMoveDuration)
                    .SetEase(tweenVectorData.Ease));
            }

            await JumpSequence.AwaitForComplete(cancellationToken: token);
        }

        public async UniTaskVoid FallAnimationAsync(TweenVectorData tweenVectorData, int direction,
            CancellationToken token)
        {
            FallSequence = DOTween.Sequence();
            foreach (var vector in tweenVectorData.Vectors)
            {
                var newVector = vector;

                newVector.x *= direction;

                FallSequence.Append(DOTween.To(() => _rigidbody.velocity,
                        newValue => _rigidbody.velocity = newValue, newVector, _toMoveDuration)
                    .SetEase(tweenVectorData.Ease));
            }

            var lastFallVector = tweenVectorData.Vectors.Last();

            lastFallVector.x *= direction;

            var lastTween = DOTween.To(() => _rigidbody.velocity,
                    newValue => _rigidbody.velocity = newValue, lastFallVector, _toMoveDuration)
                .SetEase(tweenVectorData.Ease).SetLoops(-1);

            FallSequence.Append(lastTween);

            await FallSequence.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask KnockBackOnFallAnimationAsync(CancellationToken token)
        {
            KnockBackTween = transform.DOMoveX(_knockBackOnFall * GetPlayerDirection() * -1, _knockBackOnFallDuration)
                .SetRelative(true);

            await KnockBackTween.AwaitForComplete(cancellationToken: token);
        }
    }
}