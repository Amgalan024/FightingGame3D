using MVC.Configs;
using MVC.Configs.Animation;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.Gameplay.Models
{
    public class StateModel
    {
        public PlayerAnimationData PlayerAnimationData { get; }
        public PlayerModel PlayerModel { get; }
        public PlayerAttackModel PlayerAttackModel { get; }
        public InputActionModelsContainer InputActionModelsContainer { get; }
        public StateMachineModel StateMachineModel { get; }
        public IStateMachineProxy StateMachineProxy { get; }
        public PlayerContainer OpponentContainer { get; }

        public StateModel(PlayerModel playerModel, InputActionModelsContainer inputActionModelsContainer,
            PlayerAttackModel playerAttackModel, StateMachineModel stateMachineModel, CharacterConfig characterConfig,
            IStateMachineProxy stateMachineProxy, PlayerContainer opponentContainer)
        {
            PlayerModel = playerModel;
            InputActionModelsContainer = inputActionModelsContainer;
            PlayerAttackModel = playerAttackModel;
            StateMachineModel = stateMachineModel;
            PlayerAnimationData = characterConfig.PlayerAnimationData;
            StateMachineProxy = stateMachineProxy;
            OpponentContainer = opponentContainer;
        }
    }
}