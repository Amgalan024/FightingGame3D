using System.Linq;
using MVC.Configs.Enums;

namespace MVC.Models
{
    public class ControlModelsContainer
    {
        public ControlModel[] ControlModels { get; }
        public ControlModel MoveForward { get; }
        public ControlModel MoveBackward { get; }
        public ControlModel Jump { get; }
        public ControlModel Crouch { get; }
        public ControlModel Punch { get; }
        public ControlModel Kick { get; }

        public ControlModelsContainer(ControlModel[] controlModels)
        {
            ControlModels = controlModels;

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

        private ControlModel GetControlModelByControlName(ControlNames controlName)
        {
            return ControlModels.First(x => x.Name == controlName);
        }
    }
}