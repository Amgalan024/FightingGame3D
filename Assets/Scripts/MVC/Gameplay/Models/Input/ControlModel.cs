using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Models
{
    public class ControlModel
    {
        public KeyCode Key { get; set; }
        public ControlNames Name { get; }

        public ControlModel(ControlNames name, KeyCode key)
        {
            Name = name;
            Key = key;
        }
    }
}