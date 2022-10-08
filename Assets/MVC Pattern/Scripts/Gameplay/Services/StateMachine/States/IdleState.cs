using MVC.Configs.Enums;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;

namespace MVC.StateMachine.States
{
    public class IdleState : State
    {
        private readonly JumpStateModel _jumpStateModel;
        private readonly FallStateModel _fallStateModel;
        public IdleState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage, JumpStateModel jumpStateModel, FallStateModel fallStateModel) : base(stateModel,
            playerView, storage)
        {
            _jumpStateModel = jumpStateModel;
            _fallStateModel = fallStateModel;
        }

        public override void Enter()
        {
            base.Enter();

            StateModel.InputActionModelsContainer.SetAllInputActionModels(true);

            var animationData = StateModel.CharacterConfig.PlayerAnimationData;

            _jumpStateModel.JumpTweenVectorData = animationData.GetTweenDataByDirection(animationData.JumpTweenData, DirectionType.Standing);
            _fallStateModel.FallTweenVectorData = animationData.GetTweenDataByDirection(animationData.FallTweenData, DirectionType.Standing);
        }
    }
}