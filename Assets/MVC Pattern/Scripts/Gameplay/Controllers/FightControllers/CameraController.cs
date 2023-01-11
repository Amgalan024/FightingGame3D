using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Services;
using MVC.Gameplay.Views;
using MVC.Utils.Disposable;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class CameraController : DisposableWithCts, IInitializable, IFixedTickable
    {
        private readonly CameraView _cameraView;

        private readonly List<Transform> _playerTransforms;

        public CameraController(CameraView cameraView, FightSceneStorage storage)
        {
            _cameraView = cameraView;
            _playerTransforms = storage.PlayerContainers.Select(p => p.View.transform).ToList();
        }

        void IInitializable.Initialize()
        {
            foreach (var cameraBorderView in _cameraView.IncreaseSizeBorders)
            {
                cameraBorderView.OnTriggerEntered += IncreaseCameraSize;
            }

            foreach (var cameraBorderView in _cameraView.DecreaseSizeBorders)
            {
                cameraBorderView.OnTriggerEntered += DecreaseCameraSize;
            }
        }

        void IFixedTickable.FixedTick()
        {
            HandleCameraMovement();
        }

        private void IncreaseCameraSize(Collider collider)
        {
            if (collider.GetComponent<PlayerView>())
            {
                _cameraView.SetBordersActive(false, _cameraView.IncreaseSizeBorders);
                _cameraView.SetBordersActive(true, _cameraView.DecreaseSizeBorders);

                _cameraView.IncreaseSizeAsync().Forget();
            }
        }

        private void DecreaseCameraSize(Collider collider)
        {
            if (collider.GetComponent<PlayerView>())
            {
                _cameraView.SetBordersActive(true, _cameraView.IncreaseSizeBorders);
                _cameraView.SetBordersActive(false, _cameraView.DecreaseSizeBorders);

                _cameraView.DecreaseSizeAsync().Forget();
            }
        }
фыва
        private void HandleCameraMovement()
        {
            var distanceBetweenPlayers = Mathf.Abs(_playerTransforms[0].position.x - _playerTransforms[1].position.x);

            float cameraPositionX;

            if (_playerTransforms[0].position.x > _playerTransforms[1].position.x)
            {
                cameraPositionX = _playerTransforms[0].position.x - distanceBetweenPlayers / 2;
            }
            else
            {
                cameraPositionX = _playerTransforms[0].position.x + distanceBetweenPlayers / 2;
            }

            _cameraView.MoveToPositionXAsync(cameraPositionX).Forget();
        }
    }
}