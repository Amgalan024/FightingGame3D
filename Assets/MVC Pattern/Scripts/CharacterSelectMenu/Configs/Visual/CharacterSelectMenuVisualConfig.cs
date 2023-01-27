using MVC.Menu.Views;
using MVC.Menu.Views.Network;
using UnityEngine;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterSelectMenuVisualConfig),
        menuName = "Configs/CharacterSelectMenu/" + nameof(CharacterSelectMenuVisualConfig))]
    public class CharacterSelectMenuVisualConfig : ScriptableObject
    {
        [SerializeField] private CharacterSelectMenuView _menuView;
        [SerializeField] private CharacterSelectButtonView _buttonView;

        [SerializeField] private PhotonViewHolder _photonViewHolder;
        
        public CharacterSelectButtonView ButtonView => _buttonView;
        public CharacterSelectMenuView MenuView => _menuView;

        public PhotonViewHolder PhotonViewHolder => _photonViewHolder;
    }
}