using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class LoseState : IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        public LoseState(PlayerContainer playerContainer)
        {
            PlayerContainer = playerContainer;
        }

        public void Enter()
        {
            PlayerContainer.InputFilterModelsContainer.SetAllInputActionModelFilters(false);
            Debug.Log("Lost", PlayerContainer.View);
        }

        public void Exit()
        {
        }
    }
}