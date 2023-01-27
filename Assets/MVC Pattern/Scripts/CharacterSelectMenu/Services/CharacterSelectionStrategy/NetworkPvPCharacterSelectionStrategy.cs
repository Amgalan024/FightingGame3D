using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using MVC.Configs;
using MVC.Menu.Models;
using MVC.Menu.Views.Network;
using MVC_Pattern.Scripts.MainMenu.Network;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MVC.Menu.Services.CharacterSelectionStrategy
{
    public class NetworkPvPCharacterSelectionStrategy : ICharacterSelectionStrategy
    {
        private const byte AddPlayerButtonIndexEventCode = 1;

        private const int PlayersCount = 2;

        private const int PlayerIndex1 = 0;
        private const int PlayerIndex2 = 1;

        public event Action<SelectedCharactersContainer> OnCharactersSelected;

        private readonly CharacterSelectMenuVisualConfig _visualConfig;
        private readonly CharacterSelectMenuInputConfig[] _inputConfigs;

        private readonly CharacterSelectMenuStorage _menuStorage;
        private readonly SelectedCharactersContainer _selectedCharactersContainer;

        private readonly NetworkCallbacksHandlersHolder _networkCallbacksHandlersHolder;

        private List<int> _playerButtonIndexes;

        private PhotonViewHolder _photonViewHolder;

        public NetworkPvPCharacterSelectionStrategy(CharacterSelectMenuVisualConfig visualConfig,
            CharacterSelectMenuStorage menuStorage, SelectedCharactersContainer selectedCharactersContainer,
            CharacterSelectMenuInputConfig[] inputConfigs,
            NetworkCallbacksHandlersHolder networkCallbacksHandlersHolder)
        {
            _visualConfig = visualConfig;
            _menuStorage = menuStorage;
            _selectedCharactersContainer = selectedCharactersContainer;
            _inputConfigs = inputConfigs;
            _networkCallbacksHandlersHolder = networkCallbacksHandlersHolder;
        }

        public void Initialize()
        {
            _photonViewHolder = PhotonNetwork.Instantiate(_visualConfig.PhotonViewHolder.name,
                Vector3.zero, Quaternion.identity).GetComponent<PhotonViewHolder>();

            _networkCallbacksHandlersHolder.EventCallbacksHandler.OnEvent += AddPlayerButtonIndex;

            _playerButtonIndexes = new List<int>(PlayersCount)
            {
                0,
                _menuStorage.MenuView.GridLayoutGroup.constraintCount - 1
            };

            _menuStorage.CharacterButtonViews[_playerButtonIndexes[PlayerIndex1]]
                .PlaySelectedByPlayerAnimation(PlayerIndex1);
            _menuStorage.CharacterButtonViews[_playerButtonIndexes[PlayerIndex2]]
                .PlaySelectedByPlayerAnimation(PlayerIndex2);
        }

        public void HandlePlayerSelection()
        {
            if (_photonViewHolder.PhotonView.IsMine)
            {
                HandlePlayerSelecting(PlayerIndex1, _inputConfigs[PlayerIndex1]);
            }
            else
            {
                HandlePlayerSelecting(PlayerIndex2, _inputConfigs[PlayerIndex2]);
            }
        }

        private void HandlePlayerSelecting(int playerIndex, CharacterSelectMenuInputConfig inputConfig)
        {
            if (Input.GetKeyDown(inputConfig.Down) && _playerButtonIndexes[playerIndex] <
                _menuStorage.CharacterButtonViews.Count - _menuStorage.MenuView.GridLayoutGroup.constraintCount)
            {
                RaiseAddPlayerButtonIndexEvent(playerIndex, _menuStorage.MenuView.GridLayoutGroup.constraintCount);
            }

            if (Input.GetKeyDown(inputConfig.Up) && _playerButtonIndexes[playerIndex] >
                _menuStorage.MenuView.GridLayoutGroup.constraintCount - 1)
            {
                RaiseAddPlayerButtonIndexEvent(playerIndex, -_menuStorage.MenuView.GridLayoutGroup.constraintCount);
            }

            if (Input.GetKeyDown(inputConfig.Right) &&
                _playerButtonIndexes[playerIndex] < _menuStorage.CharacterButtonViews.Count - 1)
            {
                RaiseAddPlayerButtonIndexEvent(playerIndex, 1);
            }

            if (Input.GetKeyDown(inputConfig.Left) && _playerButtonIndexes[playerIndex] > 0)
            {
                RaiseAddPlayerButtonIndexEvent(playerIndex, -1);
            }

            if (Input.GetKeyDown(inputConfig.Choose))
            {
                var selectedButton = _menuStorage.CharacterButtonViews[_playerButtonIndexes[playerIndex]];
                _selectedCharactersContainer.PlayerConfigs.Add(_menuStorage.CharacterConfigsByButtons[selectedButton]);

                if (_selectedCharactersContainer.PlayerConfigs.Count >= PlayersCount)
                {
                    OnCharactersSelected?.Invoke(_selectedCharactersContainer);
                }
            }
        }

        private void RaiseAddPlayerButtonIndexEvent(int playerIndex, int value)
        {
            var paramsArray = new int[]
            {
                playerIndex,
                value
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};

            PhotonNetwork.RaiseEvent(AddPlayerButtonIndexEventCode, paramsArray, raiseEventOptions,
                SendOptions.SendReliable);
        }

        private void AddPlayerButtonIndex(EventData eventData)
        {
            var byteCode = eventData.Code;

            if (byteCode == AddPlayerButtonIndexEventCode)
            {
                var data = (int[]) eventData.CustomData;

                var playerIndex = data[0];
                var value = data[1];

                _menuStorage.CharacterButtonViews[_playerButtonIndexes[playerIndex]].PlayUnselectedByPlayerAnimation(playerIndex);

                _playerButtonIndexes[playerIndex] += value;

                _menuStorage.CharacterButtonViews[_playerButtonIndexes[playerIndex]].PlaySelectedByPlayerAnimation(playerIndex);
            }
        }
    }
}