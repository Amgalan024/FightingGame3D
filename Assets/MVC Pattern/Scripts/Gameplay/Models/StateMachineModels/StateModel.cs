using MVC.Configs;
using MVC.Gameplay.Models.Player;
using MVC.Gameplay.Models.StateMachineModels;
using MVC.Models;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.Gameplay.Models
{
    public class StateModel
    {
        public CharacterConfig CharacterConfig { get; }
        public PlayerModel PlayerModel { get; }
        public PlayerAttackModel PlayerAttackModel { get; }
        public InputModelsContainer InputModelsContainer { get; }
        public InputActionModelsContainer InputActionModelsContainer { get; }
        public StateMachineModel StateMachineModel { get; }
        public StateMachineProxy StateMachineProxy { get; }

        public StateModel(PlayerModel playerModel, InputActionModelsContainer inputActionModelsContainer,
            PlayerAttackModel playerAttackModel, InputModelsContainer inputModelsContainer,
            StateMachineProxy stateMachineProxy, StateMachineModel stateMachineModel, CharacterConfig characterConfig)
        {
            PlayerModel = playerModel;
            InputActionModelsContainer = inputActionModelsContainer;
            PlayerAttackModel = playerAttackModel;
            InputModelsContainer = inputModelsContainer;
            StateMachineProxy = stateMachineProxy;
            StateMachineModel = stateMachineModel;
            CharacterConfig = characterConfig;
        }
    }
}