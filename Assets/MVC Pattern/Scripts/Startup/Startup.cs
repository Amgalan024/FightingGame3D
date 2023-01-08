using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Startup
{
    public class Startup : LifetimeScope
    {
        [SerializeField] private StartupConfig _startupConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_startupConfig);

            builder.RegisterEntryPoint<StartupController>();
        }
    }
}