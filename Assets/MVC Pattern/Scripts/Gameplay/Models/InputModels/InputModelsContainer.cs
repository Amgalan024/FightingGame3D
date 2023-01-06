using System.Collections.Generic;
using MVC.Configs.Enums;

namespace MVC.Models
{
    public class InputModelsContainer
    {
        public Dictionary<ControlType, InputModel> InputModelsByName { get; }

        public InputModelsContainer(List<InputData> inputDataConfig)
        {
            InputModelsByName = new Dictionary<ControlType, InputModel>(inputDataConfig.Count);

            foreach (var inputData in inputDataConfig)
            {
                var inputModel = new InputModel(inputData.Type, inputData.Key);
                InputModelsByName.Add(inputData.Type, inputModel);
            }

            var moveBackwardInputModel = InputModelsByName[ControlType.MoveBackward];

            InputModelsByName.Add(ControlType.Block, moveBackwardInputModel);
        }

        public void SwitchMovementControllers()
        {
            var forwardKey = InputModelsByName[ControlType.MoveForward].Key;
            var backwardKey = InputModelsByName[ControlType.MoveBackward].Key;

            InputModelsByName[ControlType.MoveForward].Key = backwardKey;
            InputModelsByName[ControlType.MoveBackward].Key = forwardKey;
        }

        public void SetAllInputActionModelFilters(bool value)
        {
            foreach (var inputModel in InputModelsByName.Values)
            {
                inputModel.Filter = value;
            }
        }

        public void SetAttackInputActionFilters(bool value)
        {
            InputModelsByName[ControlType.Punch].Filter = value;
            InputModelsByName[ControlType.Kick].Filter = value;
        }

        public void SetMovementInputActionFilters(bool value)
        {
            InputModelsByName[ControlType.MoveForward].Filter = value;
            InputModelsByName[ControlType.MoveBackward].Filter = value;
            InputModelsByName[ControlType.Crouch].Filter = value;
        }

        public void SetJumpInputActionFilter(bool value)
        {
            InputModelsByName[ControlType.Jump].Filter = value;
        }

        public void SetBlockInputActionFilters(bool value)
        {
            InputModelsByName[ControlType.Block].Filter = value;
        }
    }
}