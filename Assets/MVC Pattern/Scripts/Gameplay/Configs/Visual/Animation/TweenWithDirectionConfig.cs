using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Configs.Animation
{
    [Serializable]
    public class TweenWithDirectionConfig
    {
        [SerializeField] private MovementType _movementType;
        [SerializeField] private TweenConfig _tweenConfig;

        public MovementType MovementType => _movementType;
        public TweenConfig TweenConfig => _tweenConfig;
    }
}