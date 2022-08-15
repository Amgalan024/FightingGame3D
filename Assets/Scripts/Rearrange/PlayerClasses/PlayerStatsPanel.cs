using UnityEngine.UI;

public class PlayerStatsPanel
{
    private readonly PlayerModel playerModel;
    private readonly Slider _healthPointBar;
    private readonly Slider _energyPointBar;
    private readonly Text _scoreText;

    public PlayerStatsPanel(PlayerModel playerModel, Slider healthPointBar, Slider energyPointBar, Image icon,
        Text scoreText)
    {
        this.playerModel = playerModel;
        _healthPointBar = healthPointBar;
        _energyPointBar = energyPointBar;
        _scoreText = scoreText;
        icon.sprite = playerModel.Icon;
        SetStatBar(healthPointBar, playerModel.MaxHealthPoints, playerModel.HealthPoints);
        SetStatBar(energyPointBar, playerModel.MaxEnergyPoints, playerModel.EnergyPoints);
        scoreText.text = playerModel.RoundScore.ToString();
        playerModel.OnHPChanged += OnHPChanged;
        playerModel.OnPlayerWonRound += OnPlayerWonRound;
    }

    private void OnHPChanged(int healthPoints)
    {
        _healthPointBar.value = healthPoints;
    }

    private void OnPlayerWonRound(int roundScore)
    {
        _scoreText.text = roundScore.ToString();
    }

    private void SetStatBar(Slider statBar, int maxStatValue, int startStatValue)
    {
        statBar.minValue = 0;
        statBar.maxValue = maxStatValue;
        statBar.value = startStatValue;
    }
}