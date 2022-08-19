using MVC.Gameplay.Models.Player;
using MVC.Models;
using UnityEngine;

namespace MVC.Gameplay.Models
{
    public class StateModel
    {
        public PlayerModel PlayerModel { get; }
        public PlayerAttackModel PlayerAttackModel { get; }
        public StatesContainer StatesContainer { get; }
        public ControlModelsContainer ControlModelsContainer { get; }
        public InputActionModelsContainer InputActionModelsContainer { get; }

        public StateModel(StatesContainer statesContainer, PlayerModel playerModel,
            InputActionModelsContainer inputActionModelsContainer, PlayerAttackModel playerAttackModel, ControlModelsContainer controlModelsContainer)
        {
            StatesContainer = statesContainer;
            PlayerModel = playerModel;
            InputActionModelsContainer = inputActionModelsContainer;
            PlayerAttackModel = playerAttackModel;
            ControlModelsContainer = controlModelsContainer;
        }
    }
}