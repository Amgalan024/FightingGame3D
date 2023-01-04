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
        public InputActionModelsContainer InputActionModelsContainer { get; }
        public ComboModelsContainer ComboModelsContainer { get; }

        public PlayerContainer OpponentContainer { private set; get; }

        public PlayerContainer(PlayerModel model, PlayerView view, PlayerAttackModel attackModel,
            TriggerDetectorView attackHitBox, PlayerAnimationData animationData,
            InputModelsContainer inputModelsContainer, InputActionModelsContainer inputActionModelsContainer,
            ComboModelsContainer comboModelsContainer)
        {
            Model = model;
            View = view;
            AttackModel = attackModel;
            AttackHitBox = attackHitBox;
            AnimationData = animationData;
            InputModelsContainer = inputModelsContainer;
            InputActionModelsContainer = inputActionModelsContainer;
            ComboModelsContainer = comboModelsContainer;
        }

        public void SetOpponent(PlayerContainer playerContainer)
        {
            OpponentContainer = playerContainer;
        }
    }
}