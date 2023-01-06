using System;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerStatesController : IInitializable, IStartable, IFixedTickable, IDisposable
    {
        private readonly IStateMachine _stateMachine;

        private readonly PlayerModel _playerModel;
        private readonly StatesContainer _statesContainer;
        private readonly StateMachineModel _stateMachineModel;

        public PlayerStatesController(PlayerContainer playerContainer, IStateMachine stateMachine,
            StatesContainer statesContainer, StateMachineModel stateMachineModel)
        {
            _playerModel = playerContainer.Model;

            _stateMachine = stateMachine;
            _statesContainer = statesContainer;
            _stateMachineModel = stateMachineModel;
        }

        void IInitializable.Initialize()
        {
            foreach (var state in _statesContainer.States)
            {
                state.StateMachine = _stateMachine;
            }
        }

        void IStartable.Start()
        {
            _stateMachine.ChangeState<IdleState>();

            HandlePlayerEvents();
        }

        void IFixedTickable.FixedTick()
        {
            _stateMachineModel.FixedTickState?.OnFixedTick();
        }

        void IDisposable.Dispose()
        {
            DisposePlayerEvents();
        }

        private void HandlePlayerEvents()
        {
            _playerModel.OnWin += _stateMachine.ChangeState<WinState>;
            _playerModel.OnLose += _stateMachine.ChangeState<LoseState>;
        }

        private void DisposePlayerEvents()
        {
            _playerModel.OnWin -= _stateMachine.ChangeState<WinState>;
            _playerModel.OnLose -= _stateMachine.ChangeState<LoseState>;
        }
    }
}