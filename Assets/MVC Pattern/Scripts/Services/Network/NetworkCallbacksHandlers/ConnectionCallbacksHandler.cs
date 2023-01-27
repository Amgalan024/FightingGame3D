using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

namespace MVC_Pattern.Scripts.MainMenu.Network.NetworkEventHandlers
{
    public class ConnectionCallbacksHandler : IConnectionCallbacks
    {
        public event Action OnConnected;
        public event Action OnConnectedToMaster;
        public event Action<DisconnectCause> OnDisconnected;
        public event Action<RegionHandler> OnRegionListReceived;
        public event Action<Dictionary<string, object>> OnCustomAuthenticationResponse;
        public event Action<string> OnCustomAuthenticationFailed;

        void IConnectionCallbacks.OnConnected()
        {
            OnConnected?.Invoke();
        }

        void IConnectionCallbacks.OnConnectedToMaster()
        {
            OnConnectedToMaster?.Invoke();
        }

        void IConnectionCallbacks.OnDisconnected(DisconnectCause cause)
        {
            OnDisconnected?.Invoke(cause);
        }

        void IConnectionCallbacks.OnRegionListReceived(RegionHandler regionHandler)
        {
            OnRegionListReceived?.Invoke(regionHandler);
        }

        void IConnectionCallbacks.OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            OnCustomAuthenticationResponse?.Invoke(data);
        }

        void IConnectionCallbacks.OnCustomAuthenticationFailed(string debugMessage)
        {
            OnCustomAuthenticationFailed?.Invoke(debugMessage);
        }
    }
}