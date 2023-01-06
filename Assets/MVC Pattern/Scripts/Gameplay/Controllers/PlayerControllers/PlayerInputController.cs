using System;
using MVC.Gameplay.Models.Player;
using MVC.Models;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerInputController : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerModel _playerModel;

        private readonly InputModelsContainer _inputModelsContainer;
        private readonly InputActionModelsContainer _inputActionModelsContainer;

        public PlayerInputController(PlayerContainer playerContainer)
        {
            _playerModel = playerContainer.Model;
            _inputModelsContainer = playerContainer.InputModelsContainer;
            _inputActionModelsContainer = playerContainer.InputActionModelsContainer;
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
                _inputActionModelsContainer.PunchAction.InvokeInput();
            }

            if (Input.GetKeyDown(_inputModelsContainer.Kick.Key))
            {
                _inputActionModelsContainer.KickAction.InvokeInput();
            }
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(_inputModelsContainer.Jump.Key))
            {
                _inputActionModelsContainer.JumpAction.InvokeInput();
            }
        }

        private void HandleMovementInput()
        {
            if (Input.GetKey(_inputModelsContainer.MoveForward.Key))
            {
                _inputActionModelsContainer.MoveForwardAction.InvokeInput();
            }

            if (Input.GetKey(_inputModelsContainer.MoveBackward.Key))
            {
                _inputActionModelsContainer.MoveBackwardAction.InvokeInput();
            }

            if (Input.GetKey(_inputModelsContainer.Crouch.Key))
            {
                _inputActionModelsContainer.CrouchAction.InvokeInput();
            }
        }

        private void HandleBlockInput()
        {
            if (Input.GetKey(_inputModelsContainer.MoveBackward.Key))
            {
                _inputActionModelsContainer.StartBlockAction.InvokeInput();

                if (!_inputActionModelsContainer.StartBlockAction.Filter)
                {
                    _inputActionModelsContainer.StopBlockAction.InvokeInput();
                }
            }
            else
            {
                _inputActionModelsContainer.StopBlockAction.InvokeInput();
            }
        }
    }
}