using System;
using UnityEngine;

namespace MVC.Views
{
    public class CollisionDetectorView : MonoBehaviour
    {
        public event Action<Collision> OnCollisionEntered;
        public event Action<Collision> OnCollisionStaying;
        public event Action<Collision> OnCollisionExited;
        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("OnCollisionEnter");
            OnCollisionEntered?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            OnCollisionStaying?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnCollisionExited?.Invoke(collision);
        }
    }
}