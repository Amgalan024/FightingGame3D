using MVC.Menu.Configs;
using MVC_Pattern.Scripts.MainMenu.Controllers;
using MVC_Pattern.Scripts.MainMenu.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Root
{
    public class MainMenuEntryPoint : LifetimeScope
    {
        [SerializeField] private MainMenuConfig _config;
        [SerializeField] private MainMenuView _mainMenuView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_config);
            builder.RegisterInstance(_mainMenuView);

            builder.RegisterEntryPoint<MainMenuController>();
        }
    }
}