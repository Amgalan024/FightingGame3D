using MVC.Configs;
using UnityEngine;

namespace MVC.Menu.Services
{
    public class CharacterSelectMenuFactory
    {
        private readonly CharacterSelectMenuStorage _storage;
        private readonly CharacterSelectMenuVisualConfig _menuVisualConfig;

        public CharacterSelectMenuFactory(CharacterSelectMenuStorage storage,
            CharacterSelectMenuVisualConfig menuVisualConfig)
        {
            _storage = storage;
            _menuVisualConfig = menuVisualConfig;
        }

        public void CreateMenuView()
        {
            _storage.MenuView = Object.Instantiate(_menuVisualConfig.MenuView);
        }

        public void CreateCharacterSelectButtons()
        {
            foreach (var data in _menuVisualConfig.CharacterSelectButtonData)
            {
                var characterButton = Object.Instantiate(data.ButtonView, _storage.MenuView.ButtonsContainer);

                var characterConfig = data.CharacterConfig;

                _storage.CharacterButtonViews.Add(characterButton);

                _storage.CharacterConfigsByButtons.Add(characterButton, characterConfig);
            }
        }
    }
}