using System;
using UnityEngine;

namespace MVC.Views
{
    public class PlayerHitBoxView : MonoBehaviour
    {
        public event Action<Collider> OnColliderEnter;
        public event Action<Collider> OnColliderExit;

        public event Action<Collision> OnCollisionEntered;
        public event Action<Collision> OnCollisionStaying;
        public event Action<Collision> OnCollisionExited;


        public void OnTriggerEnter(Collider other)
        {
            OnColliderEnter?.Invoke(other);
        }

        public void OnTriggerExit(Collider other)
        {
            OnColliderExit?.Invoke(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
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