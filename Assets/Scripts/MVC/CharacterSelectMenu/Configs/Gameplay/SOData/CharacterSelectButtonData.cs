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
        [SerializeField] private CharacterConfig _characterConfig;

        public CharacterSelectButtonView ButtonView => _buttonView;
        public CharacterConfig CharacterConfig => _characterConfig;
    }
}