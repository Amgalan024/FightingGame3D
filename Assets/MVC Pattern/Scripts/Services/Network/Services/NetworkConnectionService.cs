using Photon.Pun;

namespace MVC_Pattern.Scripts.MainMenu.Network
{
    public class NetworkConnectionService
    {
        private readonly NetworkCallbacksHandlersHolder _networkCallbacksHandlersHolder;

        public NetworkConnectionService(NetworkCallbacksHandlersHolder networkCallbacksHandlersHolder)
        {
            _networkCallbacksHandlersHolder = networkCallbacksHandlersHolder;
        }

        public void Connect()
        {
            AddCallbackTargets();

            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }

        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
            RemoveCallbackTargets();
        }

        private void AddCallbackTargets()
        {
            PhotonNetwork.AddCallbackTarget(_networkCallbacksHandlersHolder.ConnectionCallbacksHandler);
            PhotonNetwork.AddCallbackTarget(_networkCallbacksHandlersHolder.MatchmakingCallbacksHandler);
            PhotonNetwork.AddCallbackTarget(_networkCallbacksHandlersHolder.EventCallbacksHandler);
        }

        private void RemoveCallbackTargets()
        {
            PhotonNetwork.RemoveCallbackTarget(_networkCallbacksHandlersHolder.ConnectionCallbacksHandler);
            PhotonNetwork.RemoveCallbackTarget(_networkCallbacksHandlersHolder.MatchmakingCallbacksHandler);
            PhotonNetwork.RemoveCallbackTarget(_networkCallbacksHandlersHolder.EventCallbacksHandler);
        }
    }
}