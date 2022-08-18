using System.Linq;
using MVC.Configs.Enums;

namespace MVC.Models
{
    public class ControlModelsContainer
    {
        public ControlModel[] ControlModels { get; }
        public ControlModel MoveForward { get; set; }
        public ControlModel MoveBackward { get; set; }
        public ControlModel Jump { get; set; }
        public ControlModel Crouch { get; set; }
        public ControlModel Punch { get; set; }
        public ControlModel Kick { get; set; }

        public ControlModelsContainer(ControlModel[] controlModels)
        {
            ControlModels = controlModels;

            ReassignPlayerControls();
        }

        public void ReassignPlayerControls()
        {
            MoveForward = GetControlModelByControlName(ControlNames.MoveForward);
            MoveBackward = GetControlModelByControlName(ControlNames.MoveBackward);
            Jump = GetControlModelByControlName(ControlNames.Jump);
            Crouch = GetControlModelByControlName(ControlNames.Crouch);
            Punch = GetControlModelByControlName(ControlNames.Punch);
            Kick = GetControlModelByControlName(ControlNames.Kick);
        }

        private ControlModel GetControlModelByControlName(ControlNames controlName)
        {
            return ControlModels.First(x => x.Name == controlName);
        }
    }
}