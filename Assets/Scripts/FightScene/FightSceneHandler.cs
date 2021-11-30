using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class FightSceneHandler 
{
    private Player player1;
    private Player player2;
    public FightSceneHandler(Player player1, Player player2)
    {
        this.player1 = player1;
        this.player2 = player2;
        player1.OnPlayerDied += OnPlayer1Died;
        player2.OnPlayerDied += OnPlayer2Died;
    }
    private void OnPlayer1Died()
    {
        player2.AddRoundScore();
    }
    private void OnPlayer2Died()
    {
        player1.AddRoundScore();
    }
}

