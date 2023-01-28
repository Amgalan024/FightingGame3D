using MVC.Configs.Enums;
using MVC.Models;
using UnityEngine;

namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class RunStateModel
    {
        public InputFilterModel InputFilterModel { get; private set; }
        public MovementType MovementType { get; private set; }
        public int AnimationHash { get; private set; }

        public void SetData(InputFilterModel inputFilterModel, MovementType movementType, int animationHash)
        {
            InputFilterModel = inputFilterModel;
            MovementType = movementType;
            AnimationHash = animationHash;
        }
    }
}