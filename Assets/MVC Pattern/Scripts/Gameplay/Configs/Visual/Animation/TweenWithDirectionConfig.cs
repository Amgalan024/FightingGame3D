using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Configs.Animation
{
    [Serializable]
    public class TweenWithDirectionConfig
    {
        [SerializeField] private DirectionType _directionType;
        [SerializeField] private TweenConfig _tweenConfig;

        public DirectionType DirectionType => _directionType;
        public TweenConfig TweenConfig => _tweenConfig;
    }
}