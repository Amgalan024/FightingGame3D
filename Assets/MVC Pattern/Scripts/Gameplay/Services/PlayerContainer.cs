using MVC.Configs.Animation;
using MVC.Models;
using MVC.Views;

namespace MVC.Gameplay.Models.Player
{
    public class PlayerContainer
    {
        public PlayerAnimationData AnimationData { get; }
        public PlayerModel Model { get; }
        public PlayerView View { get; }
        public PlayerAttackModel AttackModel { get; }
        public TriggerDetectorView AttackHitBox { get; }
        public InputModelsContainer InputModelsContainer { get; }
        public ComboModelsContainer ComboModelsContainer { get; }

        public PlayerContainer OpponentContainer { get; private set; }

        public PlayerContainer(PlayerModel model, PlayerView view, PlayerAttackModel attackModel,
            TriggerDetectorView attackHitBox, PlayerAnimationData animationData,
            InputModelsContainer inputModelsContainer, ComboModelsContainer comboModelsContainer)
        {
            Model = model;
            View = view;
            AttackModel = attackModel;
            AttackHitBox = attackHitBox;
            AnimationData = animationData;
            InputModelsContainer = inputModelsContainer;
            ComboModelsContainer = comboModelsContainer;
        }

        public void SetOpponent(PlayerContainer playerContainer)
        {
            OpponentContainer = playerContainer;
        }
    }
}