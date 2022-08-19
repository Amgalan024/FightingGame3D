using System;
using MVC.Models;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class BaseInputController : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerModel _playerModel;

        private readonly ControlModelsContainer _controlModelsContainer;
        private readonly InputActionModelsContainer _inputActionModelsContainer;

        public BaseInputController(InputActionModelsContainer inputActionModelsContainer,
            ControlModelsContainer controlModelsContainer, PlayerModel playerModel)
        {
            _inputActionModelsContainer = inputActionModelsContainer;
            _controlModelsContainer = controlModelsContainer;
            _playerModel = playerModel;
        }

        void IInitializable.Initialize()
        {
            _playerModel.OnPlayerTurned += _controlModelsContainer.SwitchMovementControllers;
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
            _playerModel.OnPlayerTurned -= _controlModelsContainer.SwitchMovementControllers;
        }

        private void HandleAttackInput()
        {
            if (Input.GetKeyDown(_controlModelsContainer.Punch.Key))
            {
                _inputActionModelsContainer.PunchActionModel.InvokeInput();
            }

            if (Input.GetKeyDown(_controlModelsContainer.Kick.Key))
            {
                _inputActionModelsContainer.KickActionModel.InvokeInput();
            }
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(_controlModelsContainer.Jump.Key))
            {
                _inputActionModelsContainer.JumpActionModel.InvokeInput();
            }
        }

        private void HandleMovementInput()
        {
            if (Input.GetKey(_controlModelsContainer.MoveForward.Key))
            {
                _inputActionModelsContainer.MoveForwardActionModel.InvokeInput();
            }

            if (Input.GetKey(_controlModelsContainer.MoveBackward.Key))
            {
                _inputActionModelsContainer.MoveBackwardActionModel.InvokeInput();
            }

            if (Input.GetKey(_controlModelsContainer.Crouch.Key))
            {
                _inputActionModelsContainer.CrouchActionModel.InvokeInput();
            }
        }

        private void HandleBlockInput()
        {
            if (Input.GetKey(_controlModelsContainer.MoveBackward.Key))
            {
                _inputActionModelsContainer.BlockActionModel.InvokeInput();
            }
            else
            {
                _inputActionModelsContainer.BlockStoppedActionModel.InvokeInput();
            }
        }
    }
}