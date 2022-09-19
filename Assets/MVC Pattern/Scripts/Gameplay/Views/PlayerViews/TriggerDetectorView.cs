using System;
using UnityEngine;

namespace MVC.Views
{
    public class TriggerDetectorView : MonoBehaviour
    {
        public event Action<Collider> OnColliderEnter;
        public event Action<Collider> OnColliderExit;


        public void OnTriggerEnter(Collider other)
        {
            OnColliderEnter?.Invoke(other);
        }

        public void OnTriggerExit(Collider other)
        {
            OnColliderExit?.Invoke(other);
        }
    }
}