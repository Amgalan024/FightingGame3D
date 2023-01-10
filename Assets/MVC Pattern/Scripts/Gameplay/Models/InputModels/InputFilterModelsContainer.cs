using System.Collections.Generic;
using MVC.Configs.Enums;

namespace MVC.Models
{
    public class InputFilterModelsContainer
    {
        public Dictionary<ControlType, InputFilterModel> InputFilterModelsByType { get; }

        public InputFilterModelsContainer(List<InputData> inputDataConfig)
        {
            InputFilterModelsByType = new Dictionary<ControlType, InputFilterModel>(inputDataConfig.Count);

            foreach (var inputData in inputDataConfig)
            {
                var inputModel = new InputFilterModel(inputData.Type, inputData.Key);
                InputFilterModelsByType.Add(inputData.Type, inputModel);
            }

            var moveBackwardInputModel = InputFilterModelsByType[ControlType.MoveBackward];

            InputFilterModelsByType.Add(ControlType.Block, moveBackwardInputModel);
        }

        public void SwitchMovementControllers()
        {
            var forwardKey = InputFilterModelsByType[ControlType.MoveForward].Key;
            var backwardKey = InputFilterModelsByType[ControlType.MoveBackward].Key;

            InputFilterModelsByType[ControlType.MoveForward].Key = backwardKey;
            InputFilterModelsByType[ControlType.MoveBackward].Key = forwardKey;
        }

        public void SetAllInputActionModelFilters(bool value)
        {
            foreach (var inputModel in InputFilterModelsByType.Values)
            {
                inputModel.Filter = value;
            }
        }

        public void SetAttackInputActionFilters(bool value)
        {
            InputFilterModelsByType[ControlType.Punch].Filter = value;
            InputFilterModelsByType[ControlType.Kick].Filter = value;
        }

        public void SetMovementInputActionFilters(bool value)
        {
            InputFilterModelsByType[ControlType.MoveForward].Filter = value;
            InputFilterModelsByType[ControlType.MoveBackward].Filter = value;
            InputFilterModelsByType[ControlType.Crouch].Filter = value;
        }

        public void SetJumpInputActionFilter(bool value)
        {
            InputFilterModelsByType[ControlType.Jump].Filter = value;
        }

        public void SetBlockInputActionFilters(bool value)
        {
            InputFilterModelsByType[ControlType.Block].Filter = value;
        }
    }
}