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
    public PlayerStatsPanel(Player player, Slider healthPointBar, Slider energyPointBar, Image icon)
    {
        this.player = player;
        this.healthPointBar = healthPointBar;
        this.energyPointBar = energyPointBar;
        icon.sprite = this.player.Icon;
        SetStatBar(this.healthPointBar, player.MaxHealthPoints, player.HealthPoints);
        SetStatBar(this.energyPointBar, player.MaxEnergyPoints, player.EnergyPoints);
        player.OnHPChanged += OnHPChanged;
    }
    private void SetStatBar(Slider statBar, int maxStatValue, int startStatValue)
    {
        statBar.minValue = 0;
        statBar.maxValue = maxStatValue;
        statBar.value = startStatValue;
    }
    private void OnHPChanged(int obj)
    {
        this.healthPointBar.value = obj;
    }
}
