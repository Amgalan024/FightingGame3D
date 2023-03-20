using System;
using System.Collections.Generic;
using MVC.Configs;
using MVC.Menu.Models;
using UnityEngine;

namespace MVC.Menu.Services.CharacterSelectionStrategy
{
    public class PvPCharacterSelectionStrategy : ICharacterSelectionStrategy
    {
        private const int PlayersCount = 2;

        private const int PlayerIndex1 = 0;
        private const int PlayerIndex2 = 1;

        public event Action<SelectedCharactersContainer> OnCharactersSelected;

        private readonly CharacterSelectMenuStorage _menuStorage;
        private readonly SelectedCharactersContainer _selectedCharactersContainer;
        private readonly CharacterSelectMenuInputConfig[] _inputConfigs;

        private List<int> _playerButtonIndexes;

        public PvPCharacterSelectionStrategy(CharacterSelectMenuStorage menuStorage,
            SelectedCharactersContainer selectedCharactersContainer, CharacterSelectMenuInputConfig[] inputConfigs)
        {
            _menuStorage = menuStorage;
            _selectedCharactersContainer = selectedCharactersContainer;
            _inputConfigs = inputConfigs;
        }

        void ICharacterSelectionStrategy.Initialize()
        {
            _playerButtonIndexes = new List<int>(PlayersCount)
            {
                0,
                _menuStorage.MenuView.GridLayoutGroup.constraintCount - 1
            };

            _menuStorage.CharacterButtonViews[_playerButtonIndexes[PlayerIndex1]].SelectButton(PlayerIndex1);
            _menuStorage.CharacterButtonViews[_playerButtonIndexes[PlayerIndex2]].SelectButton(PlayerIndex2);
        }

        void ICharacterSelectionStrategy.HandlePlayerSelection()
        {
            for (int i = 0; i < _inputConfigs.Length; i++)
            {
                HandlePlayerSelecting(i, _inputConfigs[i]);
            }
        }

        private void HandlePlayerSelecting(int index, CharacterSelectMenuInputConfig inputConfig)
        {
            if (Input.GetKeyDown(inputConfig.Down) && _playerButtonIndexes[index] <
                _menuStorage.CharacterButtonViews.Count - _menuStorage.MenuView.GridLayoutGroup.constraintCount)
            {
                AddPlayerButtonIndex(index, _menuStorage.MenuView.GridLayoutGroup.constraintCount);
            }

            if (Input.GetKeyDown(inputConfig.Up) && _playerButtonIndexes[index] >
                _menuStorage.MenuView.GridLayoutGroup.constraintCount - 1)
            {
                AddPlayerButtonIndex(index, -_menuStorage.MenuView.GridLayoutGroup.constraintCount);
            }

            if (Input.GetKeyDown(inputConfig.Right) &&
                _playerButtonIndexes[index] < _menuStorage.CharacterButtonViews.Count - 1)
            {
                AddPlayerButtonIndex(index, 1);
            }

            if (Input.GetKeyDown(inputConfig.Left) && _playerButtonIndexes[index] > 0)
            {
                AddPlayerButtonIndex(index, -1);
            }

            if (Input.GetKeyDown(inputConfig.Choose))
            {
                var selectedButton = _menuStorage.CharacterButtonViews[_playerButtonIndexes[index]];

                selectedButton.CommitSelect(index);

                _selectedCharactersContainer.PlayerConfigs.Add(_menuStorage.CharacterConfigsByButtons[selectedButton]);

                if (_selectedCharactersContainer.PlayerConfigs.Count >= PlayersCount)
                {
                    OnCharactersSelected?.Invoke(_selectedCharactersContainer);
                }
            }
        }

        private void AddPlayerButtonIndex(int index, int value)
        {
            _menuStorage.CharacterButtonViews[_playerButtonIndexes[index]].UnselectButton(index);

            _playerButtonIndexes[index] += value;

            _menuStorage.CharacterButtonViews[_playerButtonIndexes[index]].SelectButton(index);
        }
    }
}