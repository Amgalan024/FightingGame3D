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
        private readonly InputModelsContainer _inputModelsContainer;

        public PlayerInputController(PlayerContainer playerContainer, IStateMachine stateMachine,
            RunStateModel runStateModel)
        {
            _stateMachine = stateMachine;
            _runStateModel = runStateModel;
            _playerModel = playerContainer.Model;
            _inputModelsContainer = playerContainer.InputModelsContainer;
        }

        void IInitializable.Initialize()
        {
            _playerModel.OnTurned += _inputModelsContainer.SwitchMovementControllers;
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
            _playerModel.OnTurned -= _inputModelsContainer.SwitchMovementControllers;
        }

        private void HandleAttackInput()
        {
            if (_inputModelsContainer.InputModelsByName[ControlType.Punch].GetInputDown())
            {
                _stateMachine.ChangeState<PunchState>();
            }

            if (_inputModelsContainer.InputModelsByName[ControlType.Kick].GetInputDown())
            {
                _stateMachine.ChangeState<KickState>();
            }
        }

        private void HandleJumpInput()
        {
            if (_inputModelsContainer.InputModelsByName[ControlType.Jump].GetInputDown())
            {
                _stateMachine.ChangeState<JumpState>();
            }
        }

        private void HandleMovementInput()
        {
            if (_inputModelsContainer.InputModelsByName[ControlType.MoveForward].GetInput())
            {
                _runStateModel.SetData(_inputModelsContainer.InputModelsByName[ControlType.MoveForward].Key,
                    MovementType.Forward, PlayerAnimatorData.Forward);

                _stateMachine.ChangeState<RunState>();
            }

            if (_inputModelsContainer.InputModelsByName[ControlType.MoveBackward].GetInput())
            {
                _runStateModel.SetData(_inputModelsContainer.InputModelsByName[ControlType.MoveBackward].Key,
                    MovementType.Backward, PlayerAnimatorData.Backward);

                _stateMachine.ChangeState<RunState>();
            }

            if (_inputModelsContainer.InputModelsByName[ControlType.Crouch].GetInput())
            {
                _stateMachine.ChangeState<CrouchState>();
            }
        }

        private void HandleBlockInput()
        {
            _playerModel.IsBlocking.Value = _inputModelsContainer.InputModelsByName[ControlType.Block].GetInput();
        }
    }
}