public interface IPlayerComponent
{
    PlayerModel PlayerModel { get; }
    void InitializeComponent(PlayerModel playerModel);
}