using MVC.Configs;
using MVC.Menu.Controllers;
using MVC.Menu.Models;
using MVC.Menu.Services;
using MVC.Menu.Services.CharacterSelectionStrategy;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Root
{
    public class CharacterSelectMenuEntryPoint : LifetimeScope
    {
        [SerializeField] private CharacterSelectMenuGameplayConfig _menuGameplayConfig;
        [SerializeField] private CharacterSelectMenuVisualConfig _menuVisualConfig;

        [SerializeField] private CharacterSelectMenuInputConfig[] _menuInputConfigs;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_menuGameplayConfig);
            builder.RegisterInstance(_menuVisualConfig);
            builder.RegisterInstance(_menuInputConfigs);

            builder.Register<SelectedCharactersContainer>(Lifetime.Singleton).AsSelf();
            builder.Register<CharacterSelectMenuFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<CharacterSelectMenuStorage>(Lifetime.Singleton).AsSelf();

            builder.Register<PvPCharacterSelectionStrategy>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<CharacterSelectController>();
        }
    }
}