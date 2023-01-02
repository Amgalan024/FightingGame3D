using System;
using UnityEngine;

namespace MVC.Gameplay.Views
{
    public class CameraBorderView : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEntered;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}