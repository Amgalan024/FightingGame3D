using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Models
{
    public class InputFilterModel
    {
        public event Action OnInputDown;
        public event Action OnInputUp;

        public event Action OnNetworkInputDown;
        public event Action OnNetworkInputUp;

        public ControlType ControlType { get; }
        public KeyCode Key { get; set; }

        public bool Filter { get; set; }
        public bool IsKeyPressed { get; private set; }
        public bool IsKeyPressedWithFilter => IsKeyPressed && Filter;

        public InputFilterModel(ControlType controlType, KeyCode key)
        {
            ControlType = controlType;
            Key = key;
        }

        public void HandleInputDown()
        {
            if (Filter && Input.GetKeyDown(Key))
            {
                OnInputDown?.Invoke();
                IsKeyPressed = true;
            }
        }

        public void HandleInputUp()
        {
            if (Input.GetKeyUp(Key))
            {
                OnInputUp?.Invoke();
                IsKeyPressed = false;
            }
        }

        public void InvokeNetworkInputDown()
        {
            OnNetworkInputDown?.Invoke();
            IsKeyPressed = true;
        }

        public void InvokeNetworkInputUp()
        {
            OnNetworkInputUp?.Invoke();
            IsKeyPressed = false;
        }
    }
}