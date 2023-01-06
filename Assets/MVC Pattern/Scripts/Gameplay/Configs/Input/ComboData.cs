using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Configs.SODataModels
{
    [Serializable]
    public struct ComboData
    {
        [SerializeField] private string _name;
        [SerializeField] private int _damage;
        [SerializeField] private ControlType[] _controlNames;
        public string Name => _name;
        public int Damage => _damage;
        public ControlType[] ControlNames => _controlNames;
    }
}