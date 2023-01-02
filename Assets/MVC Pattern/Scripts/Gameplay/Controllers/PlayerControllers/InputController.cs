using System;
using MVC.Models;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class InputController : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerModel _playerModel;

        private readonly InputModelsContainer _inputModelsContainer;
        private readonly InputActionModelsContainer _inputActionModelsContainer;

        public InputController(InputActionModelsContainer inputActionModelsContainer,
            InputModelsContainer inputModelsContainer, PlayerModel playerModel)
        {
            _inputActionModelsContainer = inputActionModelsContainer;
            _inputModelsContainer = inputModelsContainer;
            _playerModel = playerModel;
        }

        void IInitializable.Initialize()
        {
            _playerModel.OnPlayerTurned += _inputModelsContainer.SwitchMovementControllers;
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
            _playerModel.OnPlayerTurned -= _inputModelsContainer.SwitchMovementControllers;
        }

        private void HandleAttackInput()
        {
            if (Input.GetKeyDown(_inputModelsContainer.Punch.Key))
            {
                _inputActionModelsContainer.PunchActionModel.InvokeInput();
            }

            if (Input.GetKeyDown(_inputModelsContainer.Kick.Key))
            {
                _inputActionModelsContainer.KickActionModel.InvokeInput();
            }
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(_inputModelsContainer.Jump.Key))
            {
                _inputActionModelsContainer.JumpActionModel.InvokeInput();
            }
        }

        private void HandleMovementInput()
        {
            if (Input.GetKey(_inputModelsContainer.MoveForward.Key))
            {
                _inputActionModelsContainer.MoveForwardActionModel.InvokeInput();
            }

            if (Input.GetKey(_inputModelsContainer.MoveBackward.Key))
            {
                _inputActionModelsContainer.MoveBackwardActionModel.InvokeInput();
            }

            if (Input.GetKey(_inputModelsContainer.Crouch.Key))
            {
                _inputActionModelsContainer.CrouchActionModel.InvokeInput();
            }
        }

        private void HandleBlockInput()
        {
            if (Input.GetKey(_inputModelsContainer.MoveBackward.Key))
            {
                _inputActionModelsContainer.StartBlockActionModel.InvokeInput();
            }
            else
            {
                _inputActionModelsContainer.StopBlockActionModel.InvokeInput();
            }
        }
    }
}