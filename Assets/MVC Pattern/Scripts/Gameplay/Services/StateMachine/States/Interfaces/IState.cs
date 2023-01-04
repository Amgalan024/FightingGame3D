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

        abstract void Enter();
        abstract void Exit();
    }
}