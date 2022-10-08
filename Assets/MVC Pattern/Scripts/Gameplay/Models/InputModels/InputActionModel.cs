using System;
using UnityEngine;

namespace MVC.Models
{
    public class InputActionModel
    {
        public event Action OnInput;

        public bool Filter { get; set; }

        public void InvokeInput()
        {
            if (Filter)
            {
                OnInput?.Invoke();
            }
        }
    }
}