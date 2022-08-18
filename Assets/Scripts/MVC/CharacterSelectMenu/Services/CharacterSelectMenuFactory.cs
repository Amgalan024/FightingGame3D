using MVC.Configs;
using UnityEngine;

namespace MVC.Menu.Services
{
    public class CharacterSelectMenuFactory
    {
        private readonly CharacterSelectMenuVisualConfig _menuVisualConfig;
        private readonly CharacterSelectMenuGameplayConfig _menuGameplayConfig;

        private readonly CharacterSelectMenuStorage _storage;

        public CharacterSelectMenuFactory(CharacterSelectMenuStorage storage,
            CharacterSelectMenuVisualConfig menuVisualConfig, CharacterSelectMenuGameplayConfig menuGameplayConfig)
        {
            _storage = storage;
            _menuVisualConfig = menuVisualConfig;
            _menuGameplayConfig = menuGameplayConfig;
        }

        public void CreateMenuView()
        {
            _storage.MenuView = Object.Instantiate(_menuVisualConfig.MenuView);
        }

        public void CreateCharacterSelectButtons()
        {
            foreach (var characterConfig in _menuGameplayConfig.CharacterConfigs)
            {
                var characterButton =
                    Object.Instantiate(_menuVisualConfig.ButtonView, _storage.MenuView.ButtonsContainer);

                _storage.CharacterButtonViews.Add(characterButton);

                _storage.CharacterConfigsByButtons.Add(characterButton, characterConfig);
            }
        }
    }
}