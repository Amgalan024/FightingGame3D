using System;
using MVC.Configs.Animation;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class RunStateModel
    {
        public KeyCode InputKey { get; set; }
        public DirectionType DirectionType { get; set; }
        public int AnimationHash { get; set; }
        public Type DashStateType { get; set; }
    }
}