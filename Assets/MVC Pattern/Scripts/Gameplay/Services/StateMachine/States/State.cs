using System;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
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

        protected StateModel StateModel { get; }
        protected PlayerView PlayerView { get; }

        private bool _debugEnabled = true;

        public State(StateModel stateModel, PlayerView playerView)
        {
            StateModel = stateModel;
            PlayerView = playerView;
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
                attackHitBox == StateModel.OpponentContainer.PlayerAttackHitBox)
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