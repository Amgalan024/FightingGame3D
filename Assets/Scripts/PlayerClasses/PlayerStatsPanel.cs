using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPanel 
{
    private Player player;
    private Slider healthPointBar;
    private Slider energyPointBar;
    private Text scoreText;
    public PlayerStatsPanel(Player player, Slider healthPointBar, Slider energyPointBar, Image icon, Text scoreText)
    {
        this.player = player;
        this.healthPointBar = healthPointBar;
        this.energyPointBar = energyPointBar;
        this.scoreText = scoreText;
        icon.sprite = this.player.Icon;
        SetStatBar(this.healthPointBar, player.MaxHealthPoints, player.HealthPoints);
        SetStatBar(this.energyPointBar, player.MaxEnergyPoints, player.EnergyPoints);
        scoreText.text = player.RoundScore.ToString();
        player.OnHPChanged += OnHPChanged;
        player.OnPlayerWonRound += OnPlayerWonRound;
    }
    private void OnHPChanged(int healthPoints)
    {
        this.healthPointBar.value = healthPoints;
    }
    private void OnPlayerWonRound(int roundScore)
    {
        scoreText.text = roundScore.ToString();
    }
    private void SetStatBar(Slider statBar, int maxStatValue, int startStatValue)
    {
        statBar.minValue = 0;
        statBar.maxValue = maxStatValue;
        statBar.value = startStatValue;
    }
}
