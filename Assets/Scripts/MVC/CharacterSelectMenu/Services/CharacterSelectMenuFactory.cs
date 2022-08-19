using MVC.Configs;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Menu.Services
{
    public class CharacterSelectMenuFactory
    {
        private readonly CharacterSelectMenuVisualConfig _menuVisualConfig;
        private readonly CharacterSelectMenuGameplayConfig _menuGameplayConfig;

        private readonly CharacterSelectMenuStorage _storage;

        private readonly Transform _parent;

        public CharacterSelectMenuFactory(CharacterSelectMenuStorage storage, LifetimeScope lifetimeScope,
            CharacterSelectMenuVisualConfig menuVisualConfig, CharacterSelectMenuGameplayConfig menuGameplayConfig)
        {
            _storage = storage;
            _menuVisualConfig = menuVisualConfig;
            _menuGameplayConfig = menuGameplayConfig;
            _parent = lifetimeScope.transform;
        }

        public void CreateMenuView()
        {
            _storage.MenuView = Object.Instantiate(_menuVisualConfig.MenuView, _parent);
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