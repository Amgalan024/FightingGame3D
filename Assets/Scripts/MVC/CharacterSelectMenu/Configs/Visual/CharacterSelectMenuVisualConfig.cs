using MVC.Menu.Configs.SOData;
using MVC.Menu.Views;
using UnityEngine;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterSelectMenuVisualConfig),
        menuName = "Configs/CharacterSelectMenu/" + nameof(CharacterSelectMenuVisualConfig))]
    public class CharacterSelectMenuVisualConfig : ScriptableObject
    {
        [SerializeField] private CharacterSelectMenuView _menuView;
        [SerializeField] private CharacterSelectButtonView _buttonView;
        public CharacterSelectButtonView ButtonView => _buttonView;
        public CharacterSelectMenuView MenuView => _menuView;
    }
}