using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class WinState : IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        public WinState(PlayerContainer playerContainer)
        {
            PlayerContainer = playerContainer;
        }

        public void Enter()
        {
            PlayerContainer.InputFilterModelsContainer.SetAllInputActionModelFilters(false);
            PlayerContainer.View.Animator.SetBool(PlayerAnimatorData.Win, true);
            Debug.Log("Won", PlayerContainer.View);
        }

        public void Exit()
        {
        }
    }
}