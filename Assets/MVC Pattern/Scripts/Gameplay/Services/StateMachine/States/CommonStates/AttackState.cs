using System;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Views;

namespace MVC.StateMachine.States.CommonStates
{
    public class AttackState : State
    {
        private IDisposable _exitStateSubscription;

        public AttackState(StateModel stateModel, PlayerView playerView, FightSceneStorage storage) : base(stateModel,
            playerView, storage)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StateModel.PlayerModel.IsAttacking.Value = true;
            _exitStateSubscription = StateModel.PlayerModel.IsAttacking.Subscribe(ExitAttackState);
        }

        public override void Exit()
        {
            base.Exit();

            _exitStateSubscription?.Dispose();
        }

        private void ExitAttackState(bool isAttacking)
        {
            if (!isAttacking)
            {
                StateModel.StateMachineProxy.ChangeState(typeof(IdleState));
            }
        }
    }
}