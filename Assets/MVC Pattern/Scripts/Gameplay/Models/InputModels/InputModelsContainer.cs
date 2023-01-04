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
            InputModels = new List<InputModel>(inputModels.Count);

            foreach (var inputModel in inputModels)
            {
                InputModels.Add(inputModel.GetCopy());
            }

            MoveForward = GetControlModel(ControlNames.MoveForward);
            MoveBackward = GetControlModel(ControlNames.MoveBackward);
            Jump = GetControlModel(ControlNames.Jump);
            Crouch = GetControlModel(ControlNames.Crouch);
            Punch = GetControlModel(ControlNames.Punch);
            Kick = GetControlModel(ControlNames.Kick);
        }

        public void SwitchMovementControllers()
        {
            var forwardKey = MoveForward.Key;
            var backwardKey = MoveBackward.Key;

            MoveForward.Key = backwardKey;
            MoveBackward.Key = forwardKey;
        }

        private InputModel GetControlModel(ControlNames controlName)
        {
            return InputModels.First(x => x.Name == controlName);
        }
    }
}