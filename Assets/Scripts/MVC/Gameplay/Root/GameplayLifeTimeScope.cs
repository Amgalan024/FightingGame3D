using MVC.Configs;
using MVC.Gameplay.Controllers;
using MVC.Gameplay.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Root
{
    public class GameplayLifeTimeScope : LifetimeScope
    {
        [SerializeField] private GameplayVisualConfig _visualConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_visualConfig);

            builder.Register<FightSceneController>(Lifetime.Singleton);
            builder.Register<PlayerLifetimeScopeFactory>(Lifetime.Scoped);
        }
    }
}