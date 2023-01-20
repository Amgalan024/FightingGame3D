using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MVC.Views;
using UnityEngine;

namespace MVC.Gameplay.Views
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private TriggerDetectorView[] _increaseSizeBorders;
        [SerializeField] private TriggerDetectorView[] _decreaseSizeBorders;
        [SerializeField] private float _zOffset;
        [SerializeField] private float _decreasedYOffset;
        [SerializeField] private float _increasedYOffset;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _increasedSize;
        [SerializeField] private float _decreasedSize;
        [SerializeField] private float _sizeChangeDuration;

        public float ZOffset => _zOffset;
        public float YOffset => _yOffset;
        public TriggerDetectorView[] IncreaseSizeBorders => _increaseSizeBorders;
        public TriggerDetectorView[] DecreaseSizeBorders => _decreaseSizeBorders;

        private float _yOffset;

        private Tween _movePositionTween;

        private void Awake()
        {
            _yOffset = _decreasedYOffset;
        }

        public void SetZPosition(float zPosition)
        {
            var position = transform.position;

            position.z = zPosition;

            transform.position = position;
        }

        public void SetBordersActive(bool isActive, params TriggerDetectorView[] cameraBorderViews)
        {
            foreach (var cameraBorderView in cameraBorderViews)
            {
                cameraBorderView.gameObject.SetActive(isActive);
            }
        }

        public async UniTask IncreaseSizeAsync()
        {
            await ChangeSizeAsync(_increasedSize, _increasedYOffset);
        }

        public async UniTask DecreaseSizeAsync()
        {
            await ChangeSizeAsync(_decreasedSize, _decreasedYOffset);
        }

        public async UniTask MoveToPositionAsync(Vector3 moveTo)
        {
            if (_movePositionTween.IsActive())
            {
                _movePositionTween.Kill();
            }

            _movePositionTween = transform.DOMove(moveTo, _moveSpeed);

            await _movePositionTween;
        }

        private async UniTask ChangeSizeAsync(float cameraSize, float yOffset)
        {
            var sequence = DOTween.Sequence();

            var cameraTween = DOTween.To(() => _camera.m_Lens.OrthographicSize,
                value => _camera.m_Lens.OrthographicSize = value, cameraSize, _sizeChangeDuration);

            var yOffsetTween = DOTween.To(() => _yOffset, value => _yOffset = value, yOffset, _sizeChangeDuration);

            sequence.Join(cameraTween);
            sequence.Join(yOffsetTween);

            await sequence;
        }
    }
}