using System;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Utils.Disposable;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public abstract class State : DisposableWithCts, IState
    {
        public event Action OnStateEntered;
        public event Action OnStateExited;
        public StateModel StateModel { get; }
        public PlayerView PlayerView { get; }
        public FightSceneStorage Storage { get; }

        private bool _debugEnabled = true;

        public State(StateModel stateModel, PlayerView playerView, FightSceneStorage storage)
        {
            StateModel = stateModel;
            PlayerView = playerView;
            Storage = storage;
        }

        public virtual void Enter()
        {
            if (_debugEnabled)
            {
                Debug.Log($"Entered {this.GetType()}");
            }

            OnStateEntered?.Invoke();
        }

        public virtual void Exit()
        {
            if (_debugEnabled)
            {
                Debug.Log($"Exit {this.GetType()}");
            }

            OnStateExited?.Invoke();
        }

        protected void HandleBlock(Collider collider)
        {
            if (collider.TryGetComponent(out TriggerDetectorView attackHitBox) &&
                attackHitBox == Storage.GetOpponentAttackViewByModel(StateModel.PlayerAttackModel))
            {
                if (StateModel.PlayerModel.IsBlocking)
                {
                    StateModel.StateMachineProxy.ChangeState<BlockState>();
                }
                else
                {
                    StateModel.PlayerModel.InvokePlayerAttacked(attackHitBox);
                    StateModel.StateMachineProxy.ChangeState<StunnedState>();
                }
            }
        }
    }
}