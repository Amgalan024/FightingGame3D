using System.Collections.Generic;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Services
{
    public class ServicesEntryPoint : LifetimeScope
    {
        [SerializeField] private List<BaseLoadingScreenView> _loadingScreenViews;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            ConfigureLoadingScreen(builder);
        }

        private void ConfigureLoadingScreen(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreenViews);
            builder.Register<LoadingScreensContainer>(Lifetime.Singleton);
            builder.Register<LoadingScreenService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}