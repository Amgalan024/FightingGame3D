using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Models
{
    [Serializable]
    public class InputModel
    {
        [field: SerializeField] public KeyCode Key { get; set; }
        [field: SerializeField] public ControlNames Name { get; set; }

        public InputModel GetCopy()
        {
            var copy = new InputModel
            {
                Key = this.Key,
                Name = this.Name
            };

            return copy;
        }
    }
}