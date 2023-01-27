using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace MVC_Pattern.Scripts.MainMenu.Network.NetworkEventHandlers
{
    public class MatchmakingCallbacksHandler : IMatchmakingCallbacks
    {
        public event Action<List<FriendInfo>> OnFriendListUpdate;

        public event Action OnCreatedRoom;
        public event Action<short, string> OnCreateRoomFailed;
        public event Action OnJoinedRoom;
        public event Action<short, string> OnJoinRoomFailed;
        public event Action<short, string> OnJoinRandomFailed;
        public event Action OnLeftRoom;

        void IMatchmakingCallbacks.OnFriendListUpdate(List<FriendInfo> friendList)
        {
            OnFriendListUpdate?.Invoke(friendList);
        }

        void IMatchmakingCallbacks.OnCreatedRoom()
        {
            OnCreatedRoom?.Invoke();
        }

        void IMatchmakingCallbacks.OnCreateRoomFailed(short returnCode, string message)
        {
            OnCreateRoomFailed?.Invoke(returnCode, message);
        }

        void IMatchmakingCallbacks.OnJoinedRoom()
        {
            OnJoinedRoom?.Invoke();
        }

        void IMatchmakingCallbacks.OnJoinRoomFailed(short returnCode, string message)
        {
            OnJoinRoomFailed?.Invoke(returnCode, message);
        }

        void IMatchmakingCallbacks.OnJoinRandomFailed(short returnCode, string message)
        {
            OnJoinRandomFailed?.Invoke(returnCode, message);
        }

        void IMatchmakingCallbacks.OnLeftRoom()
        {
            OnLeftRoom?.Invoke();
        }
    }
}