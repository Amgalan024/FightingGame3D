namespace MVC.Models
{
    public class InputActionModelsContainer
    {
        public readonly InputActionModel MoveForwardAction = new InputActionModel();
        public readonly InputActionModel MoveBackwardAction = new InputActionModel();
        public readonly InputActionModel JumpAction = new InputActionModel();
        public readonly InputActionModel CrouchAction = new InputActionModel();
        public readonly InputActionModel PunchAction = new InputActionModel();
        public readonly InputActionModel KickAction = new InputActionModel();

        public readonly InputActionModel StartBlockAction = new InputActionModel();
        public readonly InputActionModel StopBlockAction = new InputActionModel();

        public void SetAllInputActionModelFilters(bool value)
        {
            MoveForwardAction.Filter = value;
            MoveBackwardAction.Filter = value;
            JumpAction.Filter = value;
            CrouchAction.Filter = value;
            PunchAction.Filter = value;
            KickAction.Filter = value;
            StartBlockAction.Filter = value;
        }

        public void SetAttackInputActionFilters(bool value)
        {
            PunchAction.Filter = value;
            KickAction.Filter = value;
        }

        public void SetMovementInputActionFilters(bool value)
        {
            MoveForwardAction.Filter = value;
            MoveBackwardAction.Filter = value;
            CrouchAction.Filter = value;
        }

        public void SetJumpInputActionFilter(bool value)
        {
            JumpAction.Filter = value;
        }

        public void SetBlockInputActionFilters(bool value)
        {
            StartBlockAction.Filter = value;
            StopBlockAction.Filter = value;
        }
    }
}