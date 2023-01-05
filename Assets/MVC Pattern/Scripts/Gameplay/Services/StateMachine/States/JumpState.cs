using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Models;
using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class JumpState : IPlayerState, ITriggerEnterState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachineProxy StateMachineProxy { get; }

        private readonly StateMachineModel _stateMachineModel;

        private readonly JumpStateModel _jumpStateModel;
        private readonly FallStateModel _fallStateModel;

        private CancellationTokenSource _jumpCts;

        public JumpState(IStateMachineProxy stateMachineProxy, PlayerContainer playerContainer,
            JumpStateModel jumpStateModel, StateMachineModel stateMachineModel, FallStateModel fallStateModel)
        {
            StateMachineProxy = stateMachineProxy;
            PlayerContainer = playerContainer;
            _jumpStateModel = jumpStateModel;
            _stateMachineModel = stateMachineModel;
            _fallStateModel = fallStateModel;
        }

        public void Enter()
        {
            _jumpStateModel.OnJumpInterrupted += OnJumpInterrupted;

            ConfigureFallModel();

            ConfigureInputActionFilters();

            PlayJumpAnimation();
        }

        public void Exit()
        {
        }

        void ITriggerEnterState.OnTriggerEnter(Collider collider)
        {
            var opponentView = PlayerContainer.OpponentContainer.View;
            var playerView = PlayerContainer.View;

            var overlappedColliders = Physics.OverlapBox(playerView.MainTriggerDetector.TopCollider.center,
                    playerView.MainTriggerDetector.TopCollider.size)
                .FirstOrDefault(c => c == opponentView.MainTriggerDetector.BottomCollider);

            if (overlappedColliders != null)
            {
                Physics.IgnoreCollision(playerView.CollisionDetector.Collider,
                    opponentView.CollisionDetector.Collider, true);
            }
        }

        private void OnJumpInterrupted()
        {
            _jumpCts?.Cancel();
            _jumpStateModel.OnJumpInterrupted -= OnJumpInterrupted;
        }

        private void ConfigureFallModel()
        {
            var animationData = PlayerContainer.AnimationData;
            _fallStateModel.Direction = PlayerContainer.View.GetPlayerDirection();
            _fallStateModel.FallTweenConfig =
                animationData.GetTweenDataByMovementType(animationData.FallTweenData, _jumpStateModel.MovementType);
        }

        private void ConfigureInputActionFilters()
        {
            PlayerContainer.InputActionModelsContainer.SetAllInputActionModelFilters(false);

            PlayerContainer.InputActionModelsContainer.SetBlockInputActionFilters(true);
            PlayerContainer.InputActionModelsContainer.SetAttackInputActionFilters(true);
        }

        private void PlayJumpAnimation()
        {
            _jumpCts?.Cancel();
            _jumpCts?.Dispose();

            _jumpCts = new CancellationTokenSource();

            PlayJumpAnimationAsync(_jumpCts.Token).Forget();
        }

        private async UniTask PlayJumpAnimationAsync(CancellationToken token)
        {
            if (PlayerContainer.Model.CurrentJumpCount < 1)
            {
                PlayerContainer.InputActionModelsContainer.SetJumpInputActionFilter(true);
            }

            if (!_stateMachineModel.CheckPreviousStateType<CommonStates.AttackState>())
            {
                PlayerContainer.Model.CurrentJumpCount++;

                await PlayerContainer.View.PlayJumpAnimationAsync(_jumpStateModel.JumpTweenConfig,
                    _jumpStateModel.Direction, token);

                _jumpStateModel.OnJumpInterrupted -= OnJumpInterrupted;

                StateMachineProxy.ChangeState<FallState>();
            }
        }
    }
}