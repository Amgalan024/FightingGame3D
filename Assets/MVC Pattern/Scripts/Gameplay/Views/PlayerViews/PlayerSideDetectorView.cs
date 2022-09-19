using System;
using UnityEngine;

namespace MVC.Views
{
    public class PlayerSideDetectorView : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEntered;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other);
        }
    }
}