using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Services.Root
{
    public class ServicesEntryPoint : LifetimeScope
    {
        [SerializeField] private BaseServiceBuilder[] _baseServiceBuilders;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            foreach (var serviceBuilder in _baseServiceBuilders)
            {
                serviceBuilder.Build(builder);
            }
        }
    }
}