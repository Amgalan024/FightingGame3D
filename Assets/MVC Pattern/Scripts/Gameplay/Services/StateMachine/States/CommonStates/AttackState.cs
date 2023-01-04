using System;
using Cysharp.Threading.Tasks.Linq;
using MVC.Gameplay.Models.Player;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;

namespace MVC.StateMachine.States.CommonStates
{
    public class AttackState : IPlayerState
    {
        public PlayerContainer PlayerContainer { get; }
        public IStateMachineProxy StateMachineProxy { get; }

        private IDisposable _exitStateSubscription;

        public AttackState(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy)
        {
            PlayerContainer = playerContainer;
            StateMachineProxy = stateMachineProxy;
        }

        public virtual void Enter()
        {
            PlayerContainer.Model.IsAttacking.Value = true;
            _exitStateSubscription = PlayerContainer.Model.IsAttacking.Subscribe(ExitAttackState);
        }

        public virtual void Exit()
        {
            _exitStateSubscription?.Dispose();
        }

        private void ExitAttackState(bool isAttacking)
        {
            if (!isAttacking)
            {
                StateMachineProxy.ChangeState<IdleState>();
            }
        }
    }
}