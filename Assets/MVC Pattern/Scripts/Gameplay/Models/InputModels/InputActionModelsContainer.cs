namespace MVC.Models
{
    public class InputActionModelsContainer
    {
        public readonly InputActionModel MoveForwardActionModel = new InputActionModel();
        public readonly InputActionModel MoveBackwardActionModel = new InputActionModel();
        public readonly InputActionModel JumpActionModel = new InputActionModel();
        public readonly InputActionModel CrouchActionModel = new InputActionModel();
        public readonly InputActionModel PunchActionModel = new InputActionModel();
        public readonly InputActionModel KickActionModel = new InputActionModel();
        
        public readonly InputActionModel StartBlockActionModel = new InputActionModel();
        public readonly InputActionModel StopBlockActionModel = new InputActionModel();

        public void SetAllInputActionModels(bool value)
        {
            MoveForwardActionModel.Filter = value;
            MoveBackwardActionModel.Filter = value;
            JumpActionModel.Filter = value;
            CrouchActionModel.Filter = value;
            PunchActionModel.Filter = value;
            KickActionModel.Filter = value;
            StartBlockActionModel.Filter = value;
        }

        public void SetAttackInputActionsFilter(bool value)
        {
            PunchActionModel.Filter = value;
            KickActionModel.Filter = value;
        }

        public void SetMovementInputActionsFilter(bool value)
        {
            MoveForwardActionModel.Filter = value;
            MoveBackwardActionModel.Filter = value;
            CrouchActionModel.Filter = value;
        }

        public void SetJumpInputActionsFilter(bool value)
        {
            JumpActionModel.Filter = value;
        }

        public void SetBlockInputActionsFilter(bool value)
        {
            StartBlockActionModel.Filter = value;
            StopBlockActionModel.Filter = value;
        }
    }
}