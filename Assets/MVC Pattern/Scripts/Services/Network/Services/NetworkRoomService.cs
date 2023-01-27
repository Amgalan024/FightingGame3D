using Photon.Pun;
using Photon.Realtime;

namespace MVC_Pattern.Scripts.MainMenu.Network
{
    public class NetworkRoomService
    {
        public void CreateRoom(string roomName)
        {
            var roomOptions = new RoomOptions
            {
                MaxPlayers = 2
            };

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}