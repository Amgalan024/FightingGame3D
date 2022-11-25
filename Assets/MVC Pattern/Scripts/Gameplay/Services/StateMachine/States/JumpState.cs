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
    public class JumpState : State
    {
        private readonly JumpStateModel _jumpStateModel;

        private CancellationTokenSource _jumpCts;

        public JumpState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage,
            JumpStateModel jumpStateModel) : base(stateModel, playerView, storage)
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

            AwaitJump(_jumpCts.Token).Forget();
        }

        private async UniTask AwaitJump(CancellationToken token)
        {
            if (StateModel.PlayerModel.CurrentJumpCount < 1)
            {
                StateModel.InputActionModelsContainer.SetJumpInputActionsFilter(true);
            }

            if (!StateModel.StateMachineModel.ComparePreviousStateTypeEquality(typeof(CommonStates.AttackState)))
            {
                StateModel.PlayerModel.CurrentJumpCount++;

                await PlayerView.JumpAnimationAsync(_jumpStateModel.JumpTweenVectorData, _jumpStateModel.Direction,
                    token);

                StateModel.StateMachineProxy.ChangeState(typeof(FallState));
            }
        }

        private void OnJumpInterrupted()
        {
            _jumpCts?.Cancel();
            _jumpStateModel.OnJumpInterrupted -= OnJumpInterrupted;
        }

        public override void OnTriggerEnter(Collider collider)
        {
            base.OnTriggerEnter(collider);

            var opponentPlayerView = Storage.GetOpponentViewByModel(StateModel.PlayerModel);

            var overlappedColliders = Physics.OverlapBox(PlayerView.PlayerTriggerDetector.TopCollider.center,
                    PlayerView.PlayerTriggerDetector.TopCollider.size)
                .FirstOrDefault(c => c == opponentPlayerView.PlayerTriggerDetector.BottomCollider);

            if (overlappedColliders != null)
            {
                Physics.IgnoreCollision(PlayerView.CollisionDetector.Collider,
                    opponentPlayerView.CollisionDetector.Collider, true);
            }
        }
    }
}