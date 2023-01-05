using System;
using MVC.Configs.Animation;
using MVC.Configs.Enums;

namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class JumpStateModel
    {
        public event Action OnJumpInterrupted;

        public int Direction { get; set; }
        public MovementType MovementType { get; set; }
        public TweenConfig JumpTweenConfig { get; set; }

        public void InvokeJumpInterruption()
        {
            OnJumpInterrupted?.Invoke();
        }
    }
}