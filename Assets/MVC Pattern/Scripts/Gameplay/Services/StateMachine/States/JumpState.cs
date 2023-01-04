using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class JumpState : State, ITriggerEnterState
    {
        private readonly JumpStateModel _jumpStateModel;

        private CancellationTokenSource _jumpCts;

        public JumpState(StateModel stateModel, PlayerView playerView, JumpStateModel jumpStateModel) : base(stateModel,
            playerView)
        {
            _jumpStateModel = jumpStateModel;
        }

        public override void Enter()
        {
            base.Enter();

            _jumpStateModel.OnJumpInterrupted += OnJumpInterrupted;

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);

            _jumpCts?.Cancel();
            _jumpCts?.Dispose();

            _jumpCts = new CancellationTokenSource();

            AwaitJumpAnimationAsync(_jumpCts.Token).Forget();
        }

        private async UniTask AwaitJumpAnimationAsync(CancellationToken token)
        {
            if (StateModel.PlayerModel.CurrentJumpCount < 1)
            {
                StateModel.InputActionModelsContainer.SetJumpInputActionsFilter(true);
            }

            if (!StateModel.StateMachineModel.CheckPreviousStateType<CommonStates.AttackState>())
            {
                StateModel.PlayerModel.CurrentJumpCount++;

                await PlayerView.JumpAnimationAsync(_jumpStateModel.JumpTweenVectorData, _jumpStateModel.Direction,
                    token);

                StateModel.StateMachineProxy.ChangeState<FallState>();
            }
        }

        void ITriggerEnterState.OnTriggerEnter(Collider collider)
        {
            HandleBlock(collider);

            var opponentPlayerView = StateModel.OpponentContainer.PlayerView;

            var overlappedColliders = Physics.OverlapBox(PlayerView.MainTriggerDetector.TopCollider.center,
                    PlayerView.MainTriggerDetector.TopCollider.size)
                .FirstOrDefault(c => c == opponentPlayerView.MainTriggerDetector.BottomCollider);

            if (overlappedColliders != null)
            {
                Physics.IgnoreCollision(PlayerView.CollisionDetector.Collider,
                    opponentPlayerView.CollisionDetector.Collider, true);
            }
        }

        private void OnJumpInterrupted()
        {
            _jumpCts?.Cancel();
            _jumpStateModel.OnJumpInterrupted -= OnJumpInterrupted;
        }
    }
}