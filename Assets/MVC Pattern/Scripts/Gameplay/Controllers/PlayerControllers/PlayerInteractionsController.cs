using System;
using MVC.Gameplay.Models.Player;
using MVC.Gameplay.Services;
using MVC.StateMachine.States;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerInteractionsController : IInitializable, IDisposable
    {
        private readonly IStateMachine _stateMachine;

        private readonly PlayerContainer _playerContainer;
        private readonly PlayerView _playerView;
        private readonly PlayerModel _playerModel;
        private readonly PlayerContainer _opponentContainer;

        private readonly FightSceneStorage _fightSceneStorage;

        public PlayerInteractionsController(PlayerContainer playerContainer, IStateMachine stateMachine,
            FightSceneStorage fightSceneStorage)
        {
            _playerContainer = playerContainer;
            _playerView = playerContainer.View;
            _playerModel = playerContainer.Model;
            _opponentContainer = playerContainer.OpponentContainer;

            _stateMachine = stateMachine;
            _fightSceneStorage = fightSceneStorage;
        }

        void IInitializable.Initialize()
        {
            TurnPlayerToOpponent();

            HandleInteractionEvents();
        }

        void IDisposable.Dispose()
        {
            DisposeInteractionEvents();
        }

        private void HandleInteractionEvents()
        {
            _playerView.MainTriggerDetector.OnTriggerEntered += OnMainTriggerEnter;

            _playerView.CollisionDetector.OnCollisionEntered += OnCollisionEnter;
            _playerView.CollisionDetector.OnCollisionExited += OnCollisionExit;

            _playerView.SideDetectorView.OnTriggerEntered += OnSideDetectorTriggered;
            _playerView.SideDetectorView.OnTriggerExited += OnSideDetectorTriggered;
        }

        private void DisposeInteractionEvents()
        {
            _playerView.MainTriggerDetector.OnTriggerEntered -= OnMainTriggerEnter;

            _playerView.CollisionDetector.OnCollisionEntered -= OnCollisionEnter;
            _playerView.CollisionDetector.OnCollisionExited -= OnCollisionExit;

            _playerView.SideDetectorView.OnTriggerEntered -= OnSideDetectorTriggered;
            _playerView.SideDetectorView.OnTriggerExited -= OnSideDetectorTriggered;
        }

        private void OnMainTriggerEnter(Collider collider)
        {
            HandleIncomingAttack(collider);
        }

        private void HandleIncomingAttack(Collider collider)
        {
            if (collider.TryGetComponent(out TriggerDetectorView attackView) &&
                attackView == _opponentContainer.AttackHitBox)
            {
                if (_playerModel.IsBlocking)
                {
                    _stateMachine.ChangeState<BlockState>();
                }
                else
                {
                    _playerModel.TakeDamage(_fightSceneStorage.AttackModelsByView[attackView].Damage);

                    _stateMachine.ChangeState<StunnedState>();
                }
            }
        }

        private void OnSideDetectorTriggered(Collider collider)
        {
            if (collider.GetComponent<PlayerView>())
            {
                TurnPlayerToOpponent();
            }
        }

        private void TurnPlayerToOpponent()
        {
            var playerTransform = _playerContainer.View.transform;
            var opponentTransform = _opponentContainer.View.transform;

            var onTheRightSide = playerTransform.position.x > opponentTransform.position.x;
            var turnedRight = _playerModel.CurrentTurn == TurnType.TurnedRight;

            if (onTheRightSide == turnedRight)
            {
                _playerModel.TurnPlayer();
                _playerView.TurnPlayer((int) _playerModel.CurrentTurn);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlatformView>())
            {
                _playerModel.IsGrounded.Value = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlatformView>())
            {
                _playerModel.IsGrounded.Value = false;
            }
        }
    }
}