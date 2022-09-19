using System;
using MVC.Gameplay.Models;
using MVC.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface IState
    {
        public event Action OnStateEntered;
        public event Action OnStateExited;
        public bool IsActive { get; set; }
        public StateModel StateModel { get; }
        public PlayerView PlayerView { get; }

        abstract void Enter();
        abstract void OnFixedTick();
        abstract void Exit();
        abstract void OnTriggerEnter(Collider collider);
        abstract void OnTriggerExit(Collider collider);
    }
}