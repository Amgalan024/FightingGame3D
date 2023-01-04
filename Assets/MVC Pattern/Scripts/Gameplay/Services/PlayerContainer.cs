using MVC.Configs;
using MVC.Models;
using MVC.Views;

namespace MVC.Gameplay.Models.Player
{
    public class PlayerContainer
    {
        public CharacterConfig CharacterConfig { get; }
        public PlayerModel PlayerModel { get; }
        public PlayerView PlayerView { get; }
        public PlayerAttackModel PlayerAttackModel { get; }
        public TriggerDetectorView PlayerAttackHitBox { get; }
        public InputModelsContainer InputModelsContainer { get; }
        public InputActionModelsContainer ActionModelsContainer { get; }
        public ComboModelsContainer ComboModelsContainer { get; }

        public PlayerContainer(PlayerModel playerModel, PlayerView playerView, PlayerAttackModel playerAttackModel,
            TriggerDetectorView playerAttackHitBox, CharacterConfig characterConfig,
            InputModelsContainer inputModelsContainer, InputActionModelsContainer actionModelsContainer,
            ComboModelsContainer comboModelsContainer)
        {
            PlayerModel = playerModel;
            PlayerView = playerView;
            PlayerAttackModel = playerAttackModel;
            PlayerAttackHitBox = playerAttackHitBox;
            CharacterConfig = characterConfig;
            InputModelsContainer = inputModelsContainer;
            ActionModelsContainer = actionModelsContainer;
            ComboModelsContainer = comboModelsContainer;
        }
    }
}