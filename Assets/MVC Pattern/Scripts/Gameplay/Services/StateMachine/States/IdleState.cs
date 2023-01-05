using MVC.Configs.Enums;
using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States
{
    public class IdleState : IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachineProxy StateMachineProxy { get; }

        private readonly JumpStateModel _jumpStateModel;
        private readonly FallStateModel _fallStateModel;

        public IdleState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy,
            JumpStateModel jumpStateModel, FallStateModel fallStateModel)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
            _jumpStateModel = jumpStateModel;
            _fallStateModel = fallStateModel;
        }

        public void Enter()
        {
            PlayerContainer.InputActionModelsContainer.SetAllInputActionModelFilters(true);

            var animationData = PlayerContainer.AnimationData;

            _jumpStateModel.JumpTweenConfig =
                animationData.GetTweenDataByMovementType(animationData.JumpTweenData, MovementType.Standing);
            _fallStateModel.FallTweenConfig =
                animationData.GetTweenDataByMovementType(animationData.FallTweenData, MovementType.Standing);
        }

        public void Exit()
        {
        }
    }
}