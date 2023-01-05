using MVC.Configs.Enums;
using UnityEngine;

namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class RunStateModel
    {
        public KeyCode InputKey { get; private set; }
        public MovementType MovementType { get; private set; }
        public int AnimationHash { get; private set; }

        public void SetData(KeyCode inputKey, MovementType movementType, int animationHash)
        {
            InputKey = inputKey;
            MovementType = movementType;
            AnimationHash = animationHash;
        }
    }
}