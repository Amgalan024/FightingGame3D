using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Startup
{
    public class Startup : LifetimeScope
    {
        [SerializeField] private StartupConfig _startupConfig;
        [SerializeField] private StartupLoadingView _startupLoadingView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_startupConfig);
            builder.RegisterInstance(_startupLoadingView);

            builder.RegisterEntryPoint<StartupController>();
        }
    }
}