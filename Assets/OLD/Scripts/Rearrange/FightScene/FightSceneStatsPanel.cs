using UnityEngine;
using UnityEngine.UI;

public class FightSceneStatsPanel : MonoBehaviour
{
    [SerializeField] private Image player1Icon;
    [SerializeField] private Slider player1HPBar;
    [SerializeField] private Slider player1EPBar;
    [SerializeField] private Image player2Icon;
    [SerializeField] private Slider player2HPBar;
    [SerializeField] private Slider player2EPBar;
    [SerializeField] private Text player1Score;
    [SerializeField] private Text player2Score;

    private PlayerStatsPanel player1Panel;
    private PlayerStatsPanel player2Panel;

    public void InitializeStatPanel(PlayerModel player1, PlayerModel player2)
    {
        player1Panel = new PlayerStatsPanel(player1, player1HPBar, player1EPBar, player1Icon, player1Score);
        player2Panel = new PlayerStatsPanel(player2, player2HPBar, player2EPBar, player2Icon, player2Score);
    }
}