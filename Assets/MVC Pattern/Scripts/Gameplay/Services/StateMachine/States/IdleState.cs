using MVC.Configs.Enums;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Models;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class IdleState : State, ITriggerEnterState
    {
        private readonly JumpStateModel _jumpStateModel;
        private readonly FallStateModel _fallStateModel;

        public IdleState(StateModel stateModel, PlayerView playerView, JumpStateModel jumpStateModel,
            FallStateModel fallStateModel) : base(stateModel, playerView)
        {
            _jumpStateModel = jumpStateModel;
            _fallStateModel = fallStateModel;
        }

        public override void Enter()
        {
            base.Enter();

            StateModel.InputActionModelsContainer.SetAllInputActionModels(true);

            var animationData = StateModel.PlayerAnimationData;

            _jumpStateModel.JumpTweenVectorData =
                animationData.GetTweenDataByDirection(animationData.JumpTweenData, DirectionType.Standing);
            _fallStateModel.FallTweenVectorData =
                animationData.GetTweenDataByDirection(animationData.FallTweenData, DirectionType.Standing);
        }

        void ITriggerEnterState.OnTriggerEnter(Collider collider)
        {
            HandleBlock(collider);
        }
    }
}