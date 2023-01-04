using MVC.Views;

namespace MVC.Gameplay.Models.Player
{
    public class PlayerContainer
    {
        public PlayerModel PlayerModel { get; }
        public PlayerView PlayerView { get; }
        public PlayerAttackModel PlayerAttackModel { get; }
        public TriggerDetectorView PlayerAttackHitBox { get; }

        public PlayerContainer(PlayerModel playerModel, PlayerView playerView, PlayerAttackModel playerAttackModel,
            TriggerDetectorView playerAttackHitBox)
        {
            PlayerModel = playerModel;
            PlayerView = playerView;
            PlayerAttackModel = playerAttackModel;
            PlayerAttackHitBox = playerAttackHitBox;
        }
    }
}