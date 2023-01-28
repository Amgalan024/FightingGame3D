using System;
using System.Linq;
using ExitGames.Client.Photon;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC_Pattern.Scripts.MainMenu.Network;
using MVC_Pattern.Scripts.Services.Network.Utils;
using Photon.Pun;
using Photon.Realtime;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Gameplay.Controllers.PlayerControllers.Network
{
    public class NetworkInputSynchronizer : IInitializable
    {
        private readonly InputFilterModelsContainer _inputFilterModelsContainer;
        private readonly NetworkCallbacksHandlersHolder _networkCallbacksHandlersHolder;

        private readonly PlayerModel _playerModel;

        public NetworkInputSynchronizer(PlayerContainer playerContainer,
            NetworkCallbacksHandlersHolder networkCallbacksHandlersHolder)
        {
            _networkCallbacksHandlersHolder = networkCallbacksHandlersHolder;
            _inputFilterModelsContainer = playerContainer.InputFilterModelsContainer;
            _playerModel = playerContainer.Model;
        }

        public void Initialize()
        {
            _networkCallbacksHandlersHolder.EventCallbacksHandler.OnEvent += HandleInputEvent;

            foreach (var inputFilterModel in _inputFilterModelsContainer.InputFilterModelsByType)
            {
                var raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};

                var data = new string[]
                {
                    _playerModel.Index.ToString(),
                    inputFilterModel.Key.ToString()
                };

                inputFilterModel.Value.OnInputDown += () =>
                {
                    PhotonNetwork.RaiseEvent((byte) NetworkEventCodes.InputKeyDownEvent, data,
                        raiseEventOptions, SendOptions.SendReliable);
                };

                inputFilterModel.Value.OnInputUp += () =>
                {
                    PhotonNetwork.RaiseEvent((byte) NetworkEventCodes.InputKeyUpEvent, data,
                        raiseEventOptions, SendOptions.SendReliable);
                };
            }
        }

        private void HandleInputEvent(EventData eventData)
        {
            var byteCode = eventData.Code;

            switch (byteCode)
            {
                case (byte) NetworkEventCodes.InputKeyDownEvent:
                {
                    var data = (string[]) eventData.CustomData;
                    var playerIndex = Convert.ToInt32(data[0]);

                    if (playerIndex != _playerModel.Index)
                    {
                        return;
                    }

                    var inputControlType = data[1];

                    var inputFilterModel = _inputFilterModelsContainer.InputFilterModelsByType.First(input =>
                        input.Key.ToString() == inputControlType).Value;

                    inputFilterModel.InvokeNetworkInputDown();
                    break;
                }
                case (byte) NetworkEventCodes.InputKeyUpEvent:
                {
                    var data = (string[]) eventData.CustomData;
                    var playerIndex = Convert.ToInt32(data[0]);

                    if (playerIndex != _playerModel.Index)
                    {
                        return;
                    }

                    var inputControlType = data[1];

                    var inputFilterModel = _inputFilterModelsContainer.InputFilterModelsByType.First(input =>
                        input.Key.ToString() == inputControlType).Value;

                    inputFilterModel.InvokeNetworkInputUp();
                    break;
                }
            }
        }
    }
}