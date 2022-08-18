using MVC.StateMachine.States;

namespace MVC.Models
{
    public class StatesContainer
    {
        public IdleState IdleState { get; }
        public RunForwardState MoveForwardState { get; }
        public RunBackwardState MoveBackwardState { get; }
        public JumpState JumpState { get; }
        public FallState FallState { get; }
        public CrouchState CrouchState { get; }
        public PunchState PunchState { get; }
        public KickState KickState { get; }
        public ComboState ComboState { get; }
        public LoseState LoseState { get; }
        public LoseState WinState { get; }
        public BlockState BlockState { get; }

        public StatesContainer(IdleState idleState, RunForwardState moveForwardState,
            RunBackwardState moveBackwardState, JumpState jumpState, FallState fallState, CrouchState crouchState,
            PunchState punchState, KickState kickState, ComboState comboState, LoseState loseState,
            BlockState blockState, LoseState winState)
        {
            IdleState = idleState;
            MoveForwardState = moveForwardState;
            MoveBackwardState = moveBackwardState;
            JumpState = jumpState;
            FallState = fallState;
            CrouchState = crouchState;
            PunchState = punchState;
            KickState = kickState;
            ComboState = comboState;
            LoseState = loseState;
            BlockState = blockState;
            WinState = winState;
        }
    }
}