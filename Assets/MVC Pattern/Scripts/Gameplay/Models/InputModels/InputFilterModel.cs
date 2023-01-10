using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Models
{
    public class InputFilterModel
    {
        public ControlType ControlType { get; }
        public KeyCode Key { get; set; }

        public bool Filter { get; set; }

        public InputFilterModel(ControlType controlType, KeyCode key)
        {
            ControlType = controlType;
            Key = key;
        }

        public bool GetInputDown()
        {
            return Filter && Input.GetKeyDown(Key);
        }

        public bool GetInputUp()
        {
            return Filter && Input.GetKeyUp(Key);
        }

        public bool GetInput()
        {
            return Filter && Input.GetKey(Key);
        }
    }
}