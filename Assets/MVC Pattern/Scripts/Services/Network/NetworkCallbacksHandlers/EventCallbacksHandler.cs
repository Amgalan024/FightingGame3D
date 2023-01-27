using System;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace MVC_Pattern.Scripts.MainMenu.Network.NetworkEventHandlers
{
    public class EventCallbacksHandler : IOnEventCallback
    {
        public event Action<EventData> OnEvent;

        void IOnEventCallback.OnEvent(EventData photonEvent)
        {
            OnEvent?.Invoke(photonEvent);
        }
    }
}