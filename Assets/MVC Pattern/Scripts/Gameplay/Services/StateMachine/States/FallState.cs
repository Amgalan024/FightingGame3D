﻿using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class FallState : State
    {
        private readonly FallStateModel _fallStateModel;

        private IDisposable _fallSubscription;

        private CancellationTokenSource _fallCts;

        public FallState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage,
            FallStateModel fallStateModel) : base(stateModel, playerView, storage)
        {
            _fallStateModel = fallStateModel;
        }

        public override void Enter()
        {
            base.Enter();

            _fallCts?.Dispose();

            _fallCts = new CancellationTokenSource();

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);

            _fallSubscription = StateModel.PlayerModel.IsGrounded.Subscribe(OnPlayerFell);

            PlayerView.FallAnimationAsync(_fallStateModel.FallTweenVectorData, _fallCts.Token).Forget();
        }

        public override void Exit()
        {
            base.Exit();

            var opponentPlayerView = Storage.GetOpponentViewByModel(StateModel.PlayerModel);

            Physics.IgnoreCollision(PlayerView.CollisionDetector.Collider,
                opponentPlayerView.CollisionDetector.Collider, false);

            _fallCts?.Cancel();
            _fallCts?.Dispose();

            _fallSubscription?.Dispose();
        }

        public override void OnTriggerEnter(Collider collider)
        {
            base.OnTriggerEnter(collider);

            var opponentPlayerView = Storage.GetOpponentViewByModel(StateModel.PlayerModel);

            var overlappedColliders = Physics.OverlapBox(PlayerView.TriggerDetector.BottomCollider.center,
                    PlayerView.TriggerDetector.BottomCollider.size)
                .FirstOrDefault(c => c == opponentPlayerView.TriggerDetector.TopCollider);

            if (overlappedColliders != null)
            {
                opponentPlayerView.KnockBackOnFallAnimationAsync(Cts.Token).Forget();
            }
        }

        private void OnPlayerFell(bool isGrounded)
        {
            if (isGrounded)
            {
                StateModel.PlayerModel.CurrentJumpCount = 0;

                StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
            }
        }
    }
}