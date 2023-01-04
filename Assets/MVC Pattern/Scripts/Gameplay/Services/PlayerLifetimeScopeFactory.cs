using MVC.Controllers;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC.Gameplay.Models.StateMachineModels;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using VContainer;
using VContainer.Unity;

namespace MVC.Gameplay.Services
{
    public class PlayerLifetimeScopeFactory
    {
        private readonly LifetimeScope _gameplayLifeTimeScope;

        public PlayerLifetimeScopeFactory(LifetimeScope gameplayLifeTimeScope)
        {
            _gameplayLifeTimeScope = gameplayLifeTimeScope;
        }

        public LifetimeScope CreatePlayerLifetimeScope(PlayerContainer playerContainer)
        {
            var scope = _gameplayLifeTimeScope.CreateChild(builder =>
            {
                builder.RegisterInstance(playerContainer.Model);
                builder.RegisterInstance(playerContainer.View);
                builder.RegisterInstance(playerContainer.AttackModel);

                builder.RegisterInstance(playerContainer);

                builder.Register<StatesContainer>(Lifetime.Scoped);
                builder.Register<StateMachineModel>(Lifetime.Scoped);

                builder.Register<InputModelsContainer>(Lifetime.Scoped);
                builder.Register<InputActionModelsContainer>(Lifetime.Scoped);
                builder.Register<ComboModelsContainer>(Lifetime.Scoped);

                builder.Register<StateMachineProxy>(Lifetime.Scoped).AsImplementedInterfaces();
                builder.Register<StateMachine.StateMachine>(Lifetime.Scoped).AsImplementedInterfaces();

                builder.RegisterEntryPoint<StateMachineController>(Lifetime.Scoped);
                builder.RegisterEntryPoint<PlayerStatesController>(Lifetime.Scoped);
                builder.RegisterEntryPoint<InputController>(Lifetime.Scoped);
                builder.RegisterEntryPoint<ComboInputController>(Lifetime.Scoped);

                BuildStates(builder);

                BuildStateModels(builder);
            });

            return scope;
        }

        private void BuildStates(IContainerBuilder builder)
        {
            builder.Register<BlockState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<ComboState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<CrouchState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<LoseState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<FallState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<IdleState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<JumpState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<KickState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<PunchState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<RunState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<DashForwardState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<DashBackwardState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<StunnedState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        }

        private void BuildStateModels(IContainerBuilder builder)
        {
            builder.Register<FallStateModel>(Lifetime.Scoped);
            builder.Register<JumpStateModel>(Lifetime.Scoped);
            builder.Register<RunStateModel>(Lifetime.Scoped);
        }
    }
}