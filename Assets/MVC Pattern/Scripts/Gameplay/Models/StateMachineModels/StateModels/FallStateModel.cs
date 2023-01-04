using MVC.Configs.Animation;

namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class FallStateModel
    {
        public int Direction { get; set; }
        public TweenConfig FallTweenConfig { get; set; }
    }
}