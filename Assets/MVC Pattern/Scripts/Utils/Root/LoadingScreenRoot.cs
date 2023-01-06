using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using VContainer;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Utils.Root
{
    public class LoadingScreenRoot : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<LoadingScreenService>(Lifetime.Singleton).AsImplementedInterfaces();
            
            DontDestroyOnLoad(this);
        }
    }
}