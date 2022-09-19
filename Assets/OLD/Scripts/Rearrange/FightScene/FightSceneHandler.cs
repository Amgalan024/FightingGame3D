using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FightSceneHandler
{
    private PlayerModel player1;
    private PlayerModel player2;

    public FightSceneHandler(PlayerModel player1, PlayerModel player2)
    {
        player1 = player1;
        player2 = player2;
        player1.OnLose += OnPlayer1Died;
        player2.OnLose += OnPlayer2Died;
    }

    private void OnPlayer1Died()
    {
        player2.ScoreWin();
    }

    private void OnPlayer2Died()
    {
        player1.ScoreWin();
    }
}