using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MVC.Gameplay.Views
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private CameraBorderView[] _smallSizeBorders;
        [SerializeField] private CameraBorderView[] _bigSizeBorders;
        [SerializeField] private float _cameraMoveSpeed;
        [SerializeField] private float _increasedSize;
        [SerializeField] private float _decreasedSize;
        [SerializeField] private float _sizeChangeDuration;

        public CameraBorderView[] SmallSizeBorders => _smallSizeBorders;
        public CameraBorderView[] BigSizeBorders => _bigSizeBorders;

        public void SetBordersActive(bool isActive, params CameraBorderView[] cameraBorderViews)
        {
            foreach (var cameraBorderView in cameraBorderViews)
            {
                cameraBorderView.SetActive(isActive);
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

        public async UniTask MoveToPositionXAsync(float posX)
        {
            await transform.DOMoveX(posX, _cameraMoveSpeed);
        }

        private async UniTask ChangeSizeAsync(float size)
        {
            await DOTween.To(() => _camera.m_Lens.OrthographicSize, value => _camera.m_Lens.OrthographicSize = value,
                size, _sizeChangeDuration);
        }
    }
}