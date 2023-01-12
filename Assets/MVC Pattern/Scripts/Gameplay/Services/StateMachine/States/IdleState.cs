using MVC.Configs.Enums;
using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class IdleState : IPlayerState, IFixedTickState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        private readonly JumpStateModel _jumpStateModel;
        private readonly FallStateModel _fallStateModel;

        public IdleState(PlayerContainer playerContainer, JumpStateModel jumpStateModel, FallStateModel fallStateModel)
        {
            PlayerContainer = playerContainer;
            _jumpStateModel = jumpStateModel;
            _fallStateModel = fallStateModel;
        }

        public void Enter()
        {
            PlayerContainer.InputFilterModelsContainer.SetAllInputActionModelFilters(true);

            var animationData = PlayerContainer.AnimationData;
            
            _jumpStateModel.MovementType = MovementType.Standing;
            _jumpStateModel.JumpTweenConfig =
                animationData.GetTweenDataByMovementType(animationData.JumpTweenData, MovementType.Standing);
            
            _fallStateModel.FallTweenConfig =
                animationData.GetTweenDataByMovementType(animationData.FallTweenData, MovementType.Standing);
        }

        public void Exit()
        {
        }

        void IFixedTickState.OnFixedTick()
        {
            PlayerContainer.View.Rigidbody.velocity = Vector3.zero;
        }
    }
}