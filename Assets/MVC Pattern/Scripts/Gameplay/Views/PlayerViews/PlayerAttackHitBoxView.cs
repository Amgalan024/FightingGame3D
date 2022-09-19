using System;
using UnityEngine;

namespace MVC.Views
{
    public class PlayerAttackHitBoxView : MonoBehaviour
    {
        public event Action<Collider> OnColliderHit;

        public void OnTriggerEnter(Collider other)
        {
            OnColliderHit?.Invoke(other);
        }
    }
}