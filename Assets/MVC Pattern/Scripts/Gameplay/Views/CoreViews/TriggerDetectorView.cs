using System;
using UnityEngine;

namespace MVC.Views
{
    public class TriggerDetectorView : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEntered;
        public event Action<Collider> OnTriggerStaying;
        public event Action<Collider> OnTriggerExited;

        [SerializeField] private Collider _collider;

        public Collider Collider => _collider;

        public void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerStaying?.Invoke(other);
        }

        public void OnTriggerExit(Collider other)
        {
            OnTriggerExited?.Invoke(other);
        }
    }
}