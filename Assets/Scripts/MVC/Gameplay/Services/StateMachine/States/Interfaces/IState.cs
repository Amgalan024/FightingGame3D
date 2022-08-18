using MVC.Gameplay.Models;
using MVC.Models;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface IState
    {
        public bool IsActive { get; set; }
        public StateModel StateModel { get; }
        public StateMachineModel StateMachineModel { get; }
        public PlayerView PlayerView { get; }

        abstract void Enter();
        abstract void Exit();
        abstract void OnTriggerEnter(Collider collider);
        abstract void OnTriggerExit(Collider collider);
    }
}