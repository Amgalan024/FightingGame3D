using System;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class RunStateModel
    {
        public KeyCode InputKey { get; private set; }
        public DirectionType DirectionType { get; private set; }
        public int AnimationHash { get; private set; }
        public Type DashStateType { get; private set; }

        public void SetData(KeyCode inputKey, DirectionType directionType, int animationHash,
            Type dashStateType)
        {
            InputKey = inputKey;
            DirectionType = directionType;
            AnimationHash = animationHash;
            DashStateType = dashStateType;
        }
    }
}