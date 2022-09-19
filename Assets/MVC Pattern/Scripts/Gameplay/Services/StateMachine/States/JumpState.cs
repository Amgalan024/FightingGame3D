using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVC.Configs.Enums;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;
using UnityEngine;

namespace MVC.StateMachine.States
{
    public class JumpState : State
    {
        private CancellationTokenSource _fallCts;

        public JumpState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateModel.InputActionModelsContainer.SetAllInputActionModels(false);

            StateModel.InputActionModelsContainer.SetBlockInputActionsFilter(true);
            StateModel.InputActionModelsContainer.SetAttackInputActionsFilter(true);

            if (!StateModel.StateMachineModel.ComparePreviousStateTypeEquality(typeof(CommonStates.AttackState)))
            {
                if (StateModel.StateMachineModel.ComparePreviousStateTypeEquality(typeof(RunForwardState)))
                {
                    PlayerView.MovingJumpAnimationAsync(DirectionType.Forward, Token).Forget();
                }
                else if (StateModel.StateMachineModel.ComparePreviousStateTypeEquality(typeof(RunBackwardState)))
                {
                    PlayerView.MovingJumpAnimationAsync(DirectionType.Backward, Token).Forget();
                }
                else
                {
                    PlayerView.StandingJumpAnimationAsync(Token).Forget();
                }

                StateModel.PlayerModel.CurrentJumpCount++;
                Debug.Log("CurrentJumpCount = ++ = " +  StateModel.PlayerModel.CurrentJumpCount);

            }

            if (StateModel.PlayerModel.CurrentJumpCount < 2)
            {
                StateModel.InputActionModelsContainer.SetJumpInputActionsFilter(true);
            }

            _fallCts = new CancellationTokenSource();

            AwaitFall(_fallCts.Token).Forget();
        }

        public override void Exit()
        {
            base.Exit();

            _fallCts.Cancel();
            _fallCts.Dispose();
        }

        private async UniTask AwaitFall(CancellationToken token)
        {
            await UniTask.WaitUntil(() => PlayerView.Rigidbody.velocity.y <= 0, cancellationToken: token);
            StateModel.StateMachineProxy.ChangeState(typeof(FallState));
        }
    }
}