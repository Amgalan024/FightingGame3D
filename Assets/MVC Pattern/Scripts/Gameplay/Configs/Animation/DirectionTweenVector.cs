using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Configs.Animation
{
    [Serializable]
    public class DirectionTweenVector
    {
        [SerializeField] private DirectionType _directionType;
        [SerializeField] private TweenVectorData _tweenVectorData;

        public DirectionType DirectionType => _directionType;
        public TweenVectorData TweenVectorData => _tweenVectorData;
    }
}