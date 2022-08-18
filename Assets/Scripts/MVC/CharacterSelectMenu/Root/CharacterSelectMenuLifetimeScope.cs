using MVC.Configs;
using MVC.Menu.Controllers;
using MVC.Menu.Models;
using MVC.Menu.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Root
{
    public class CharacterSelectMenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private CharacterSelectMenuVisualConfig _characterSelectMenuVisualConfig;

        [SerializeField] private CharacterSelectMenuInputConfig[] _menuInputConfigs;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_characterSelectMenuVisualConfig);
            builder.RegisterInstance(_menuInputConfigs);

            builder.Register<SelectedCharactersContainer>(Lifetime.Singleton).AsSelf();
            builder.Register<CharacterSelectMenuFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<CharacterSelectMenuStorage>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<CharacterSelectController>();
        }
    }
}