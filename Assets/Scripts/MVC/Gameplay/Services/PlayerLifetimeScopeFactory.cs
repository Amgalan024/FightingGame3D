using MVC.Controllers;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.StateMachine;
using MVC.StateMachine.States;
using MVC.Views;
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

        public LifetimeScope CreatePlayerLifetimeScope(PlayerModel playerModel, PlayerView playerView)
        {
            var scope = _gameplayLifeTimeScope.CreateChild(builder =>
            {
                builder.RegisterInstance(playerModel);
                builder.RegisterInstance(playerView);

                builder.Register<StateModel>(Lifetime.Scoped);
                builder.Register<StatesContainer>(Lifetime.Scoped);
                builder.Register<StateMachineModel>(Lifetime.Scoped);

                builder.Register<PlayerAttackModel>(Lifetime.Scoped);

                builder.Register<ControlModelsContainer>(Lifetime.Scoped);
                builder.Register<InputActionModelsContainer>(Lifetime.Scoped);
                builder.Register<ComboModelsContainer>(Lifetime.Scoped);

                builder.Register<PlayerStateMachine>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();

                builder.RegisterEntryPoint<PlayerStateController>(Lifetime.Scoped);
                builder.RegisterEntryPoint<BaseInputController>(Lifetime.Scoped);
                builder.RegisterEntryPoint<ComboInputController>(Lifetime.Scoped);

                BuildStates(builder);
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
            builder.Register<RunBackwardState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<RunForwardState>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        }
    }
}