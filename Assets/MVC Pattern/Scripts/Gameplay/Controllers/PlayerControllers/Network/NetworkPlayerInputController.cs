using System;
using MVC.Configs.Enums;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC.Menu.Views.Network;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Gameplay.Controllers.PlayerControllers.Network
{
    public class NetworkPlayerInputController : IInitializable, ITickable, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private readonly RunStateModel _runStateModel;

        private readonly PlayerModel _playerModel;
        private readonly InputFilterModelsContainer _inputFilterModelsContainer;


        public NetworkPlayerInputController(PlayerContainer playerContainer, IStateMachine stateMachine,
            RunStateModel runStateModel)
        {
            _stateMachine = stateMachine;
            _runStateModel = runStateModel;
            _playerModel = playerContainer.Model;
            _inputFilterModelsContainer = playerContainer.InputFilterModelsContainer;
        }

        void IInitializable.Initialize()
        {
            HandleAttackInput();
            HandleJumpInput();
            HandleMovementInput();
        }

        void ITickable.Tick()
        {
            foreach (var inputFilterModel in _inputFilterModelsContainer.InputFilterModelsByType)
            {
                inputFilterModel.Value.HandleInputDown();
                inputFilterModel.Value.HandleInputUp();
            }

            HandleBlockInput();
        }

        void IDisposable.Dispose()
        {
        }

        private void HandleAttackInput()
        {
            _inputFilterModelsContainer.InputFilterModelsByType[ControlType.Punch].OnNetworkInputDown +=
                _stateMachine.ChangeState<PunchState>;

            _inputFilterModelsContainer.InputFilterModelsByType[ControlType.Kick].OnNetworkInputDown +=
                _stateMachine.ChangeState<KickState>;
        }

        private void HandleJumpInput()
        {
            _inputFilterModelsContainer.InputFilterModelsByType[ControlType.Jump].OnNetworkInputDown +=
                _stateMachine.ChangeState<JumpState>;
        }

        private void HandleMovementInput()
        {
            _inputFilterModelsContainer.InputFilterModelsByType[ControlType.MoveForward].OnNetworkInputDown += () =>
            {
                _runStateModel.SetData(_inputFilterModelsContainer.InputFilterModelsByType[ControlType.MoveForward],
                    MovementType.Forward, PlayerAnimatorData.Forward);

                _stateMachine.ChangeState<RunState>();
            };

            _inputFilterModelsContainer.InputFilterModelsByType[ControlType.MoveBackward].OnNetworkInputDown += () =>
            {
                _runStateModel.SetData(
                    _inputFilterModelsContainer.InputFilterModelsByType[ControlType.MoveBackward],
                    MovementType.Backward, PlayerAnimatorData.Backward);

                _stateMachine.ChangeState<RunState>();
            };

            _inputFilterModelsContainer.InputFilterModelsByType[ControlType.Crouch].OnNetworkInputDown += () =>
            {
                _stateMachine.ChangeState<CrouchState>();
            };
        }

        private void HandleBlockInput()
        {
            _playerModel.IsBlocking.Value =
                _inputFilterModelsContainer.InputFilterModelsByType[ControlType.Block].IsKeyPressedWithFilter;
        }
    }
}