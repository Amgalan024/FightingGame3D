using VContainer;

namespace MVC_Pattern.Scripts.Services.SceneLoader.Builders
{
    public class SceneLoadServiceBuilder : BaseServiceBuilder
    {
        public override void Build(IContainerBuilder builder)
        {
            builder.Register<SceneLoadService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}