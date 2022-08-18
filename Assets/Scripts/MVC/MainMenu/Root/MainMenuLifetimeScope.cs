using MVC.Menu.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC.Root
{
    public class MainMenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private MainMenuVisualConfig _visualConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance(_visualConfig);
        }
    }
}