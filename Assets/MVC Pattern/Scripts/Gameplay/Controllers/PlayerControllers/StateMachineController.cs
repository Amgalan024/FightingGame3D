using System;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.StateMachineModels;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class StateMachineController : IInitializable, IFixedTickable, IDisposable
    {
        private readonly StateMachine.StateMachine _stateMachine;
        private readonly StateMachineProxy _stateMachineProxy;
        private readonly StateMachineModel _stateMachineModel;

        private readonly PlayerView _playerView;

        public StateMachineController(StateMachineModel stateMachineModel, StateMachine.StateMachine stateMachine,
            PlayerView playerView, StateMachineProxy stateMachineProxy)
        {
            _stateMachineModel = stateMachineModel;
            _stateMachine = stateMachine;
            _playerView = playerView;
            _stateMachineProxy = stateMachineProxy;
        }

        void IInitializable.Initialize()
        {
            _playerView.MainTriggerDetector.OnTriggerEntered += OnTriggerEntered;
            _playerView.MainTriggerDetector.OnTriggerExited += OnTriggerExited;

            _stateMachineProxy.OnStateChanged += _stateMachine.ChangeState;
        }

        void IFixedTickable.FixedTick()
        {
            _stateMachineModel.FixedTickState?.OnFixedTick();
        }

        void IDisposable.Dispose()
        {
            _playerView.MainTriggerDetector.OnTriggerEntered -= OnTriggerEntered;
            _playerView.MainTriggerDetector.OnTriggerExited -= OnTriggerExited;

            _stateMachineProxy.OnStateChanged -= _stateMachine.ChangeState;
        }

        private void OnTriggerEntered(Collider collider)
        {
            _stateMachineModel.TriggerEnterState?.OnTriggerEnter(collider);
        }

        private void OnTriggerExited(Collider collider)
        {
            _stateMachineModel.TriggerExitState?.OnTriggerExit(collider);
        }
    }
}