using System;
using UnityEngine;

namespace MVC.Views
{
    public class TriggerDetectorView : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEntered;
        public event Action<Collider> OnTriggerExited;

        [SerializeField] private BoxCollider _bottomCollider;
        [SerializeField] private BoxCollider _topCollider;

        public BoxCollider BottomCollider => _bottomCollider;
        public BoxCollider TopCollider => _topCollider;

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