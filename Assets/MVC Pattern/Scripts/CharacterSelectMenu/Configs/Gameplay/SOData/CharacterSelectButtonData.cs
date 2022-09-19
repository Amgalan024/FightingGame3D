using System;
using MVC.Configs;
using MVC.Menu.Views;
using UnityEngine;

namespace MVC.Menu.Configs.SOData
{
    [Serializable]
    public class CharacterSelectButtonData
    {
        [SerializeField] private CharacterSelectButtonView _buttonView;
        public CharacterSelectButtonView ButtonView => _buttonView;
        [SerializeField] private CharacterConfig _characterConfig;

        public CharacterConfig CharacterConfig => _characterConfig;
    }
}