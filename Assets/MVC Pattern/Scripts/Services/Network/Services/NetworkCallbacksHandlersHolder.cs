using MVC_Pattern.Scripts.MainMenu.Network.NetworkEventHandlers;
using Photon.Pun;

namespace MVC_Pattern.Scripts.MainMenu.Network
{
    public class NetworkCallbacksHandlersHolder
    {
        public readonly ConnectionCallbacksHandler ConnectionCallbacksHandler;
        public readonly MatchmakingCallbacksHandler MatchmakingCallbacksHandler;
        public readonly EventCallbacksHandler EventCallbacksHandler;

        public NetworkCallbacksHandlersHolder(ConnectionCallbacksHandler connectionCallbacksHandler,
            MatchmakingCallbacksHandler matchmakingCallbacksHandler, EventCallbacksHandler eventCallbacksHandler)
        {
            ConnectionCallbacksHandler = connectionCallbacksHandler;
            MatchmakingCallbacksHandler = matchmakingCallbacksHandler;
            EventCallbacksHandler = eventCallbacksHandler;
        }
    }
}