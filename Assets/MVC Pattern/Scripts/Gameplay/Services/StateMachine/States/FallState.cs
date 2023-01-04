using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using MVC.Gameplay.Models.Player;
using MVC.Utils.Disposable;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class FallState : DisposableWithCts, IPlayerState, ITriggerEnterState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachineProxy StateMachineProxy { get; }

        private readonly FallStateModel _fallStateModel;

        private IDisposable _fallSubscription;

        private CancellationTokenSource _fallCts;

        public FallState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy,
            FallStateModel fallStateModel)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
            _fallStateModel = fallStateModel;
        }

        public void Enter()
        {
            _fallCts?.Dispose();

            _fallCts = new CancellationTokenSource();

            PlayerContainer.InputActionModelsContainer.SetAllInputActionModels(false);

            PlayerContainer.InputActionModelsContainer.SetBlockInputActionsFilter(true);

            _fallSubscription = PlayerContainer.Model.IsGrounded.Subscribe(OnPlayerFell);

            PlayerContainer.View.FallAnimationAsync(_fallStateModel.FallTweenVectorData,
                _fallStateModel.Direction,
                _fallCts.Token).Forget();
        }

        public void Exit()
        {
            var opponentPlayerView = PlayerContainer.OpponentContainer.View;

            Physics.IgnoreCollision(PlayerContainer.View.CollisionDetector.Collider,
                opponentPlayerView.CollisionDetector.Collider, false);

            _fallCts?.Cancel();
            _fallCts?.Dispose();

            _fallSubscription?.Dispose();
        }

        void ITriggerEnterState.OnTriggerEnter(Collider collider)
        {
            var opponentPlayerView = PlayerContainer.OpponentContainer.View;

            var overlappedColliders = Physics.OverlapBox(
                PlayerContainer.View.MainTriggerDetector.BottomCollider.transform.position,
                PlayerContainer.View.MainTriggerDetector.BottomCollider.size);

            var topCollider =
                overlappedColliders.FirstOrDefault(c => c == opponentPlayerView.MainTriggerDetector.TopCollider);

            if (topCollider != null && !opponentPlayerView.KnockBackTween.IsActive())
            {
                opponentPlayerView.KnockBackOnFallAnimationAsync(Cts.Token).Forget();
            }
        }

        private void OnPlayerFell(bool isGrounded)
        {
            if (isGrounded)
            {
                PlayerContainer.Model.CurrentJumpCount = 0;

                StateMachineProxy.ChangeState<IdleState>();
            }
        }
    }
}