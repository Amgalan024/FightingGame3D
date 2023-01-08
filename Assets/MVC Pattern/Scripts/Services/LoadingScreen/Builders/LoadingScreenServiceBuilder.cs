using System.Collections.Generic;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;
using UnityEngine;
using VContainer;

namespace MVC_Pattern.Scripts.Services
{
    public class LoadingScreenServiceBuilder : BaseServiceBuilder
    {
        [SerializeField] private BaseLoadingScreenView[] _loadingScreenViews;

        public override void Build(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreenViews);
            builder.Register<LoadingScreensContainer>(Lifetime.Singleton);
            builder.Register<LoadingScreenService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}