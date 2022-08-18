using MVC.Menu.Configs.SOData;
using MVC.Menu.Views;
using UnityEngine;

namespace MVC.Configs
{
    public class CharacterSelectMenuVisualConfig : ScriptableObject
    {
        [SerializeField] private CharacterSelectMenuView _menuView;
        [SerializeField] private CharacterSelectButtonData[] _characterSelectButtonData;

        public CharacterSelectMenuView MenuView => _menuView;
        public CharacterSelectButtonData[] CharacterSelectButtonData => _characterSelectButtonData;
    }
}