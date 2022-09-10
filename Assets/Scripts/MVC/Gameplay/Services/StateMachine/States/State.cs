using System;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.StateMachineModels;
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
        public bool IsActive { get; set; }
        public StateModel StateModel { get; }
        public PlayerView PlayerView { get; }
        public FightSceneStorage Storage { get; }

        public State(StateModel stateModel, PlayerView playerView, FightSceneStorage storage)
        {
            StateModel = stateModel;
            PlayerView = playerView;
            Storage = storage;
        }

        public virtual void Enter()
        {
            OnStateEntered?.Invoke();
        }

        public virtual void OnFixedTick()
        {
        }

        public virtual void Exit()
        {
            OnStateExited?.Invoke();
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerAttackHitBoxView attackHitBox))
            {
                if (StateModel.PlayerModel.IsBlocking)
                {
                    StateModel.StateMachineProxy.ChangeState(typeof(BlockState));
                }
                else
                {
                    StateModel.PlayerModel.InvokePlayerAttacked(attackHitBox);
                }
            }
        }

        public virtual void OnTriggerExit(Collider collider)
        {
        }
    }
}