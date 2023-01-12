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

        private float _initialYPosition;

        public CameraController(CameraView cameraView, FightSceneStorage storage)
        {
            _cameraView = cameraView;
            _playerTransforms = storage.PlayerContainers.Select(p => p.View.transform).ToList();
        }

        void IInitializable.Initialize()
        {
            _initialYPosition = _cameraView.transform.position.y;

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

        private void HandleCameraMovement()
        {
            Vector3 cameraPosition;

            var positionXDifference = Mathf.Abs(_playerTransforms[0].position.x - _playerTransforms[1].position.x);

            if (_playerTransforms[0].position.x > _playerTransforms[1].position.x)
            {
                cameraPosition.x = _playerTransforms[0].position.x - positionXDifference / 2;
            }
            else
            {
                cameraPosition.x = _playerTransforms[0].position.x + positionXDifference / 2;
            }

            var positionYDifference = Mathf.Abs(_playerTransforms[0].position.y - _playerTransforms[1].position.y);

            cameraPosition.y = _initialYPosition + positionYDifference / 2;

            cameraPosition.z = _cameraView.transform.position.z;

            _cameraView.MoveToPositionAsync(cameraPosition).Forget();
        }
    }
}