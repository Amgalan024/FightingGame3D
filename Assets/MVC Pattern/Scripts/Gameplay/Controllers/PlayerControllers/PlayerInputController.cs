using System;
using MVC.Configs.Enums;
using MVC.Gameplay.Constants;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerInputController : IInitializable, ITickable, IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private readonly RunStateModel _runStateModel;

        private readonly PlayerModel _playerModel;
        private readonly InputFilterModelsContainer _inputFilterModelsContainer;

        public PlayerInputController(PlayerContainer playerContainer, IStateMachine stateMachine,
            RunStateModel runStateModel)
        {
            _stateMachine = stateMachine;
            _runStateModel = runStateModel;
            _playerModel = playerContainer.Model;
            _inputFilterModelsContainer = playerContainer.InputFilterModelsContainer;
        }

        void IInitializable.Initialize()
        {
            _playerModel.OnTurned += _inputFilterModelsContainer.SwitchMovementControllers;
        }

        void ITickable.Tick()
        {
            HandleMovementInput();
            HandleAttackInput();
            HandleJumpInput();
            HandleBlockInput();
        }

        void IDisposable.Dispose()
        {
            _playerModel.OnTurned -= _inputFilterModelsContainer.SwitchMovementControllers;
        }

        private void HandleAttackInput()
        {
            if (_inputFilterModelsContainer.InputFilterModelsByType[ControlType.Punch].GetInputDown())
            {
                _stateMachine.ChangeState<PunchState>();
            }

            if (_inputFilterModelsContainer.InputFilterModelsByType[ControlType.Kick].GetInputDown())
            {
                _stateMachine.ChangeState<KickState>();
            }
        }

        private void HandleJumpInput()
        {
            if (_inputFilterModelsContainer.InputFilterModelsByType[ControlType.Jump].GetInputDown())
            {
                _stateMachine.ChangeState<JumpState>();
            }
        }

        private void HandleMovementInput()
        {
            if (_inputFilterModelsContainer.InputFilterModelsByType[ControlType.MoveForward].GetInput())
            {
                _runStateModel.SetData(_inputFilterModelsContainer.InputFilterModelsByType[ControlType.MoveForward].Key,
                    MovementType.Forward, PlayerAnimatorData.Forward);

                _stateMachine.ChangeState<RunState>();
            }

            if (_inputFilterModelsContainer.InputFilterModelsByType[ControlType.MoveBackward].GetInput())
            {
                _runStateModel.SetData(_inputFilterModelsContainer.InputFilterModelsByType[ControlType.MoveBackward].Key,
                    MovementType.Backward, PlayerAnimatorData.Backward);

                _stateMachine.ChangeState<RunState>();
            }

            if (_inputFilterModelsContainer.InputFilterModelsByType[ControlType.Crouch].GetInput())
            {
                _stateMachine.ChangeState<CrouchState>();
            }
        }

        private void HandleBlockInput()
        {
            _playerModel.IsBlocking.Value = _inputFilterModelsContainer.InputFilterModelsByType[ControlType.Block].GetInput();
        }
    }
}