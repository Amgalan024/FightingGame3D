using DG.Tweening;
using UnityEngine;

namespace MVC.Configs.Animation
{
    public class TweenVectorData : ScriptableObject
    {
        [SerializeField] private Vector3[] _vectors;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;
        [SerializeField] private bool _isRelative;

        public Vector3[] Vectors => _vectors;
        public Ease Ease => _ease;
        public float Duration => _duration;
        public float StepDuration => _duration / Vectors.Length;
        public bool IsRelative => _isRelative;
    }
}