using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC.Views;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerAnimatorController : IInitializable, IDisposable
    {
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>(5);

        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;

        public PlayerAnimatorController(PlayerContainer playerContainer)
        {
            _playerModel = playerContainer.Model;
            _playerView = playerContainer.View;
        }

        void IInitializable.Initialize()
        {
            HandlePlayerEvents();
        }

        void IDisposable.Dispose()
        {
            DisposePlayerEvents();
        }

        private void HandlePlayerEvents()
        {
            _playerView.OnAttackAnimationEnded += OnAttackAnimationEnded;

            _subscriptions.Add(_playerModel.IsAttacking.Subscribe(isAttacking =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsAttacking, isAttacking)));

            _subscriptions.Add(_playerModel.IsGrounded.Subscribe(isGrounded =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsGrounded, isGrounded)));

            _subscriptions.Add(_playerModel.IsCrouching.Subscribe(isCrouching =>
                _playerView.Animator.SetBool(PlayerAnimatorData.IsCrouching, isCrouching)));
        }

        private void DisposePlayerEvents()
        {
            _playerView.OnAttackAnimationEnded -= OnAttackAnimationEnded;

            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private void OnAttackAnimationEnded()
        {
            _playerModel.IsAttacking.Value = false;
        }
    }
}