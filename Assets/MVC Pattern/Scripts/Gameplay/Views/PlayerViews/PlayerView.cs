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

        [SerializeField] private TriggerDetectorView _mainTriggerDetector;
        [SerializeField] private TriggerDetectorView _attackHitBoxView;
        [SerializeField] private TriggerDetectorView _sideDetectorView;
        [SerializeField] private CollisionDetectorView _collisionDetector;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BoxCollider _bottomCollider;
        [SerializeField] private BoxCollider _topCollider;
        [SerializeField] private float _toMoveFloat;
        [SerializeField] private float _toMoveDuration;
        [SerializeField] private float _knockBackOnFall;
        [SerializeField] private float _knockBackOnFallDuration = 0.2f;

        public TriggerDetectorView MainTriggerDetector => _mainTriggerDetector;
        public TriggerDetectorView AttackHitBoxView => _attackHitBoxView;
        public TriggerDetectorView SideDetectorView => _sideDetectorView;
        public CollisionDetectorView CollisionDetector => _collisionDetector;
        public Animator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        public BoxCollider BottomCollider => _bottomCollider;
        public BoxCollider TopCollider => _topCollider;

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

        public async UniTask PlayDashForwardAnimationAsync()
        {
        }

        public async UniTask PlayDashBackwardAnimationAsync()
        {
        }

        public int GetPlayerDirection()
        {
            return (int) transform.localScale.z;
        }

        public void TurnPlayer(int direction)
        {
            var scale = transform.localScale;
            scale.z = direction;
            transform.localScale = scale;
        }

        public async UniTask PlayIdleToMoveAnimationAsync(int moveHash, CancellationToken token)
        {
            await UniTask.WaitUntil(() => !MoveToIdleTween.IsActive(), cancellationToken: token);

            IdleToMoveTween = DOTween.To(() => _animator.GetFloat(moveHash),
                newFloat => _animator.SetFloat(moveHash, newFloat), _toMoveFloat,
                _toMoveDuration);

            await IdleToMoveTween.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask PlayMoveToIdleAnimationAsync(int moveHash, CancellationToken token)
        {
            MoveToIdleTween = DOTween.To(() => _animator.GetFloat(moveHash),
                newFloat => _animator.SetFloat(moveHash, newFloat), 0, _toMoveDuration);

            await MoveToIdleTween.AwaitForComplete(cancellationToken: token);
        }

        public async UniTask PlayJumpAnimationAsync(TweenConfig tweenConfig, int direction,
            CancellationToken token)
        {
            if (JumpSequence.IsActive())
            {
                JumpSequence?.Kill();
            }

            JumpSequence = DOTween.Sequence();

            foreach (var vector in tweenConfig.Vectors)
            {
                var newVector = vector;

                newVector.x *= direction;

                JumpSequence.Append(DOTween.To(() => _rigidbody.velocity,
                        newValue => _rigidbody.velocity = newValue, newVector, _toMoveDuration)
                    .SetEase(tweenConfig.Ease));
            }

            await JumpSequence.AwaitForComplete(cancellationToken: token);
        }

        public async UniTaskVoid PlayFallAnimationAsync(TweenConfig tweenConfig, int direction,
            CancellationToken token)
        {
            FallSequence = DOTween.Sequence();
            foreach (var vector in tweenConfig.Vectors)
            {
                var newVector = vector;

                newVector.x *= direction;

                FallSequence.Append(DOTween.To(() => _rigidbody.velocity,
                        newValue => _rigidbody.velocity = newValue, newVector, _toMoveDuration)
                    .SetEase(tweenConfig.Ease));
            }

            var lastFallVector = tweenConfig.Vectors.Last();

            lastFallVector.x *= direction;

            var lastTween = DOTween.To(() => _rigidbody.velocity,
                    newValue => _rigidbody.velocity = newValue, lastFallVector, _toMoveDuration)
                .SetEase(tweenConfig.Ease).SetLoops(-1);

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