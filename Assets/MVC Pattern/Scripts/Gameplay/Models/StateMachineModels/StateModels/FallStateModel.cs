using MVC.Configs.Animation;
using MVC.Configs.Enums;

namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class FallStateModel
    {
        public int Direction { get; set; }
        public TweenVectorData FallTweenVectorData { get; set; }
    }
}