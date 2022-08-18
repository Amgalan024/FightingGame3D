using MVC.Models;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class BaseInputController : ITickable
    {
        private readonly ControlModelsContainer _controlModelsContainer;
        private readonly InputActionModelsContainer _inputActionModelsContainer;

        public BaseInputController(InputActionModelsContainer inputActionModelsContainer, ControlModelsContainer controlModelsContainer)
        {
            _inputActionModelsContainer = inputActionModelsContainer;
            _controlModelsContainer = controlModelsContainer;
        }

        public void Tick()
        {
            HandleMovementInput();
            HandleAttackInput();
            HandleJumpInput();
            HandleBlockInput();
        }

        private void HandleAttackInput()
        {
            if (Input.GetKeyDown(_controlModelsContainer.Punch.Key))
            {
                _inputActionModelsContainer.InvokePunch();
            }

            if (Input.GetKeyDown(_controlModelsContainer.Kick.Key))
            {
                _inputActionModelsContainer.InvokeKick();
            }
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(_controlModelsContainer.Jump.Key))
            {
                _inputActionModelsContainer.InvokeJump();
            }
        }

        private void HandleMovementInput()
        {
            if (Input.GetKey(_controlModelsContainer.MoveForward.Key))
            {
                _inputActionModelsContainer.InvokeMoveForward();
            }

            if (Input.GetKey(_controlModelsContainer.MoveBackward.Key))
            {
                _inputActionModelsContainer.InvokeMoveBackward();
            }

            if (Input.GetKey(_controlModelsContainer.Crouch.Key))
            {
                _inputActionModelsContainer.InvokeCrouch();
            }
        }

        private void HandleBlockInput()
        {
            if (Input.GetKey(_controlModelsContainer.MoveBackward.Key))
            {
                _inputActionModelsContainer.InvokeBlocking();
            }
            else
            {
                _inputActionModelsContainer.InvokeBlockStop();
            }
        }
    }
}