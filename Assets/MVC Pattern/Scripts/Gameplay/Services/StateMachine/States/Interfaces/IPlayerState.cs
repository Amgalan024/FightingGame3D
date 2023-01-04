using System;
using MVC.Gameplay.Models.Player;
using MVC.Utils.Disposable;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public interface IPlayerState : IState
    {
        PlayerContainer PlayerContainer { get; }
        IStateMachineProxy StateMachineProxy { get; }
    }
}