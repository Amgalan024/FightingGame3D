using System;
using UnityEngine;

namespace MVC.Views
{
    public class TriggerDetectorView : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEntered;
        public event Action<Collider> OnTriggerExited;



        public void OnTriggerEnter(Collider other)
        {
            OnTriggerEntered?.Invoke(other);
        }

        public void OnTriggerExit(Collider other)
        {
            OnTriggerExited?.Invoke(other);
        }
    }
}