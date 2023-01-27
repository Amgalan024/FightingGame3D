using MVC_Pattern.Scripts.NetworkMenu.Config;
using MVC_Pattern.Scripts.NetworkMenu.Controllers;
using MVC_Pattern.Scripts.NetworkMenu.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.NetworkMenu.Root
{
    public class NetworkMenuEntryPoint : LifetimeScope
    {
        [SerializeField] private NetworkMenuConfig _networkMenuConfig;
        [SerializeField] private NetworkMenuView _networkMenuView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_networkMenuView);
            builder.RegisterInstance(_networkMenuConfig);

            builder.RegisterEntryPoint<NetworkMenuController>();
        }
    }
}