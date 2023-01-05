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

        private readonly JumpStateModel _jumpStateModel;
        private readonly StateMachineModel _stateMachineModel;

        private CancellationTokenSource _jumpCts;

        public JumpState(IStateMachineProxy stateMachineProxy, PlayerContainer playerContainer,
            JumpStateModel jumpStateModel, StateMachineModel stateMachineModel)
        {
            StateMachineProxy = stateMachineProxy;
            PlayerContainer = playerContainer;
            _jumpStateModel = jumpStateModel;
            _stateMachineModel = stateMachineModel;
        }

        public void Enter()
        {
            _jumpStateModel.OnJumpInterrupted += OnJumpInterrupted;

            PlayerContainer.InputActionModelsContainer.SetAllInputActionModels(false);

            PlayerContainer.InputActionModelsContainer.SetBlockInputActionsFilter(true);
            PlayerContainer.InputActionModelsContainer.SetAttackInputActionsFilter(true);

            _jumpCts?.Cancel();
            _jumpCts?.Dispose();

            _jumpCts = new CancellationTokenSource();

            AwaitJumpAnimationAsync(_jumpCts.Token).Forget();
        }

        public void Exit()
        {
        }

        private async UniTask AwaitJumpAnimationAsync(CancellationToken token)
        {
            if (PlayerContainer.Model.CurrentJumpCount < 1)
            {
                PlayerContainer.InputActionModelsContainer.SetJumpInputActionsFilter(true);
            }

            if (!_stateMachineModel.CheckPreviousStateType<CommonStates.AttackState>())
            {
                PlayerContainer.Model.CurrentJumpCount++;

                await PlayerContainer.View.JumpAnimationAsync(_jumpStateModel.JumpTweenConfig,
                    _jumpStateModel.Direction, token);

                StateMachineProxy.ChangeState<FallState>();
            }
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
    }
}