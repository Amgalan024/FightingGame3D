using MVC_Pattern.Scripts.MainMenu.Network.NetworkEventHandlers;
using MVC_Pattern.Scripts.Services;
using VContainer;

namespace MVC_Pattern.Scripts.MainMenu.Network
{
    public class NetworkServiceBuilder : BaseServiceBuilder
    {
        public override void Build(IContainerBuilder builder)
        {
            builder.Register<ConnectionCallbacksHandler>(Lifetime.Singleton).AsSelf();
            builder.Register<MatchmakingCallbacksHandler>(Lifetime.Singleton).AsSelf();
            builder.Register<EventCallbacksHandler>(Lifetime.Singleton).AsSelf();

            builder.Register<NetworkRoomService>(Lifetime.Singleton).AsSelf();
            builder.Register<NetworkCallbacksHandlersHolder>(Lifetime.Singleton).AsSelf();
            builder.Register<NetworkConnectionService>(Lifetime.Singleton).AsSelf();
        }
    }
}