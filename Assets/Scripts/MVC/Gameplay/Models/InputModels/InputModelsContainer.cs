using System.Collections.Generic;
using System.Linq;
using MVC.Configs.Enums;

namespace MVC.Models
{
    public class InputModelsContainer
    {
        public List<InputModel> InputModels { get; }
        public InputModel MoveForward { get; }
        public InputModel MoveBackward { get; }
        public InputModel Jump { get; }
        public InputModel Crouch { get; }
        public InputModel Punch { get; }
        public InputModel Kick { get; }

        public InputModelsContainer(List<InputModel> inputModels)
        {
            InputModels = inputModels;

            MoveForward = GetControlModelByControlName(ControlNames.MoveForward);
            MoveBackward = GetControlModelByControlName(ControlNames.MoveBackward);
            Jump = GetControlModelByControlName(ControlNames.Jump);
            Crouch = GetControlModelByControlName(ControlNames.Crouch);
            Punch = GetControlModelByControlName(ControlNames.Punch);
            Kick = GetControlModelByControlName(ControlNames.Kick);
        }

        public void SwitchMovementControllers()
        {
            var forwardKey = MoveForward.Key;
            var backwardKey = MoveBackward.Key;

            MoveForward.Key = backwardKey;
            MoveBackward.Key = forwardKey;
        }

        private InputModel GetControlModelByControlName(ControlNames controlName)
        {
            return InputModels.First(x => x.Name == controlName);
        }
    }
}