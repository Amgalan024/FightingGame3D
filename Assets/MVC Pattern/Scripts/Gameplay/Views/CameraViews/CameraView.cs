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

        [SerializeField] private float _cameraMoveSpeed;
        [SerializeField] private float _increasedSize;
        [SerializeField] private float _decreasedSize;
        [SerializeField] private float _sizeChangeDuration;

        public TriggerDetectorView[] IncreaseSizeBorders => _increaseSizeBorders;
        public TriggerDetectorView[] DecreaseSizeBorders => _decreaseSizeBorders;

        public void SetBordersActive(bool isActive, params TriggerDetectorView[] cameraBorderViews)
        {
            foreach (var cameraBorderView in cameraBorderViews)
            {
                cameraBorderView.gameObject.SetActive(isActive);
            }
        }

        public async UniTask IncreaseSizeAsync()
        {
            await ChangeSizeAsync(_increasedSize);
        }

        public async UniTask DecreaseSizeAsync()
        {
            await ChangeSizeAsync(_decreasedSize);
        }

        public async UniTask MoveToPositionAsync(Vector3 moveTo)
        {
            await transform.DOMove(moveTo, _cameraMoveSpeed);
        }

        private async UniTask ChangeSizeAsync(float size)
        {
            await DOTween.To(() => _camera.m_Lens.OrthographicSize, value => _camera.m_Lens.OrthographicSize = value,
                size, _sizeChangeDuration);
        }
    }
}