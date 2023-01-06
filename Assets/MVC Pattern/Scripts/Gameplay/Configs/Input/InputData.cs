using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Models
{
    [Serializable]
    public struct InputData
    {
        [field: SerializeField] public KeyCode Key { get; private set; }
        [field: SerializeField] public ControlType Type { get; private set; }
    }
}