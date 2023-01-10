using MVC.Gameplay.Models;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class StateMachineController : IInitializable, IStartable, IFixedTickable
    {
        private readonly IStateMachine _stateMachine;

        private readonly StatesContainer _statesContainer;
        private readonly StateMachineModel _stateMachineModel;

        public StateMachineController(IStateMachine stateMachine, StatesContainer statesContainer,
            StateMachineModel stateMachineModel)
        {
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
        }

        void IFixedTickable.FixedTick()
        {
            _stateMachineModel.FixedTickState?.OnFixedTick();
        }
    }
}