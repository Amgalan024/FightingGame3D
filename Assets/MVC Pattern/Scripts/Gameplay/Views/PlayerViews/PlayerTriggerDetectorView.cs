using System;
using UnityEngine;

namespace MVC.Views
{
    public class PlayerTriggerDetectorView : MonoBehaviour
    {
        public event Action<Collider> OnColliderEnter;
        public event Action<Collider> OnColliderExit;

        [SerializeField] private BoxCollider _bottomCollider;
        [SerializeField] private BoxCollider _topCollider;

        public BoxCollider BottomCollider => _bottomCollider;
        public BoxCollider TopCollider => _topCollider;

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