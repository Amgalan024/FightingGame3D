using System;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class StateMachineController : IInitializable, IFixedTickable
    {
        private readonly IStateMachine _stateMachine;
        private readonly StatesContainer _statesContainer;
        private readonly StateMachineModel _stateMachineModel;

        public StateMachineController(StateMachineModel stateMachineModel,
            IStateMachine stateMachine, StatesContainer statesContainer)
        {
            _stateMachineModel = stateMachineModel;
            _stateMachine = stateMachine;
            _statesContainer = statesContainer;
        }

        void IInitializable.Initialize()
        {
            foreach (var state in _statesContainer.States)
            {
                state.StateMachine = _stateMachine;
            }
        }

        void IFixedTickable.FixedTick()
        {
            _stateMachineModel.FixedTickState?.OnFixedTick();
        }
    }
}