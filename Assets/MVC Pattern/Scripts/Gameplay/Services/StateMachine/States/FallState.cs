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
    public class FallState : DisposableWithCts, IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachine StateMachine { get; set; }

        private readonly FallStateModel _fallStateModel;

        private IDisposable _fallSubscription;

        private CancellationTokenSource _fallCts;

        public FallState(PlayerContainer playerContainer, FallStateModel fallStateModel)
        {
            PlayerContainer = playerContainer;
            _fallStateModel = fallStateModel;
        }

        public void Enter()
        {
            PlayerContainer.View.MainTriggerDetector.OnTriggerEntered += OnTriggerEnter;

            _fallCts?.Dispose();

            _fallCts = new CancellationTokenSource();

            PlayerContainer.InputActionModelsContainer.SetAllInputActionModelFilters(false);

            PlayerContainer.InputActionModelsContainer.SetBlockInputActionFilters(true);

            _fallSubscription = PlayerContainer.Model.IsGrounded.Subscribe(OnPlayerFell);

            PlayerContainer.View.PlayFallAnimationAsync(_fallStateModel.FallTweenConfig, _fallStateModel.Direction,
                _fallCts.Token).Forget();
        }

        public void Exit()
        {
            PlayerContainer.View.MainTriggerDetector.OnTriggerEntered -= OnTriggerEnter;

            var opponentPlayerView = PlayerContainer.OpponentContainer.View;

            Physics.IgnoreCollision(PlayerContainer.View.CollisionDetector.Collider,
                opponentPlayerView.CollisionDetector.Collider, false);

            _fallCts?.Cancel();
            _fallCts?.Dispose();

            _fallSubscription?.Dispose();
        }

        private void OnTriggerEnter(Collider collider)
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

                StateMachine.ChangeState<IdleState>();
            }
        }
    }
}