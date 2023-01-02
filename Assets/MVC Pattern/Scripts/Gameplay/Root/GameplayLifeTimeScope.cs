using MVC.Configs;
using MVC.Gameplay.Controllers;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Root
{
    public class GameplayLifeTimeScope : LifetimeScope
    {
        [SerializeField] private GameplayVisualConfig _visualConfig;
        [SerializeField] private PlayerInputConfig[] _playerInputConfigs;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_visualConfig);
            builder.RegisterInstance(_playerInputConfigs);

            builder.Register<PlayerLifetimeScopeFactory>(Lifetime.Singleton);

            builder.Register<FightSceneFactory>(Lifetime.Singleton);
            builder.Register<FightSceneStorage>(Lifetime.Singleton);
            builder.Register<FightSceneModel>(Lifetime.Singleton);

            builder.RegisterEntryPoint<FightSceneController>();
        }
    }
}