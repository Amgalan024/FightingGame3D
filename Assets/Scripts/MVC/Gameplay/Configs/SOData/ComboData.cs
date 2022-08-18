using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Configs.SODataModels
{
    [Serializable]
    public class ComboData
    {
        [SerializeField] private string _name;
        [SerializeField] private int _damage;
        [SerializeField] private ControlNames[] _controlNames;
        public string Name => _name;
        public int Damage => _damage;
        public ControlNames[] ControlNames => _controlNames;
    }
}