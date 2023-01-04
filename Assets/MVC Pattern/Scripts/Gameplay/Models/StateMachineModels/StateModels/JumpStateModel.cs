using System;
using MVC.Configs.Animation;

namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class JumpStateModel
    {
        public event Action OnJumpInterrupted;
        
        public int Direction { get; set; }
        public TweenConfig JumpTweenConfig { get; set; }

        public void InvokeJumpInterruption()
        {
            OnJumpInterrupted?.Invoke();
        }
    }
}