using System.Collections.Generic;
using System.Linq;
using MVC.Gameplay.Services;
using MVC.Gameplay.Views;
using MVC.Utils.Disposable;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class CameraController : DisposableWithCts, IInitializable, ITickable
    {
        private readonly CameraView _cameraView;

        private readonly List<Transform> _playerTransforms;

        public CameraController(CameraView cameraView, FightSceneStorage storage)
        {
            _cameraView = cameraView;
            _playerTransforms = storage.PlayerContainers.Select(p => p.PlayerView.transform).ToList();
        }

        void IInitializable.Initialize()
        {
            foreach (var cameraBorderView in _cameraView.SmallSizeBorders)
            {
                cameraBorderView.OnTriggerEntered += OnSmallBorderTriggerEntered;
            }

            foreach (var cameraBorderView in _cameraView.BigSizeBorders)
            {
                cameraBorderView.OnTriggerEntered += OnBigBorderTriggerEntered;
            }
        }

        void ITickable.Tick()
        {
            HandleCameraMovement();
        }

        private void OnSmallBorderTriggerEntered(Collider collider)
        {
            if (collider.GetComponent<PlayerView>())
            {
                _cameraView.SetBordersActive(false, _cameraView.SmallSizeBorders);
                _cameraView.SetBordersActive(true, _cameraView.BigSizeBorders);

                _cameraView.IncreaseSizeAsync();
            }
        }

        private void OnBigBorderTriggerEntered(Collider collider)
        {
            if (collider.GetComponent<PlayerView>())
            {
                _cameraView.SetBordersActive(true, _cameraView.SmallSizeBorders);
                _cameraView.SetBordersActive(false, _cameraView.BigSizeBorders);

                _cameraView.DecreaseSizeAsync();
            }
        }

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

            _cameraView.MoveToPositionXAsync(cameraPositionX);
        }
    }
}