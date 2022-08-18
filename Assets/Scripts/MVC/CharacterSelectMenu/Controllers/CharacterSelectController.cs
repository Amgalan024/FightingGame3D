using System.Collections.Generic;
using System.Linq;
using MVC.Configs;
using MVC.Menu.Models;
using MVC.Menu.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace MVC.Menu.Controllers
{
    public class CharacterSelectController : IInitializable, ITickable
    {
        private readonly CharacterSelectMenuStorage _menuStorage;
        private readonly CharacterSelectMenuFactory _menuFactory;
        private readonly SelectedCharactersContainer _selectedCharactersContainer;
        private readonly CharacterSelectMenuInputConfig[] _inputConfigs;

        private List<int> _playerButtonIndexes;

        private float _countDownTime;

        public CharacterSelectController(CharacterSelectMenuStorage menuStorage, CharacterSelectMenuFactory menuFactory,
            SelectedCharactersContainer selectedCharactersContainer, CharacterSelectMenuInputConfig[] inputConfigs)
        {
            _menuStorage = menuStorage;
            _menuFactory = menuFactory;
            _selectedCharactersContainer = selectedCharactersContainer;
            _inputConfigs = inputConfigs;
        }

        public void Initialize()
        {
            _menuFactory.CreateMenuView();
            _menuFactory.CreateCharacterSelectButtons();

            _playerButtonIndexes = new List<int>(2)
            {
                [0] = 0,
                [1] = _menuStorage.MenuView.GridLayoutGroup.constraintCount - 1
            };
        }

        public void Tick()
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
                _selectedCharactersContainer.PlayerConfigs.Add(_menuStorage.CharacterConfigsByButtons[selectedButton]);
                StartFightScene();
            }
        }

        private void AddPlayerButtonIndex(int index, int value)
        {
            _menuStorage.CharacterButtonViews[_playerButtonIndexes[index]].UnselectButton(index);

            _playerButtonIndexes[index] = value;

            _menuStorage.CharacterButtonViews[_playerButtonIndexes[index]].SelectButton(index);
        }

        private void StartFightScene()
        {
            if (_selectedCharactersContainer.PlayerConfigs.All(characterConfig => characterConfig != null))
            {
                _countDownTime -= Time.deltaTime;

                if (_countDownTime <= 0)
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
    }
}