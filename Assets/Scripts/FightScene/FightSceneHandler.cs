using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class FightSceneHandler : MonoBehaviour
{
    [SerializeField] private FightSceneStatsPanel fightSceneStatsPanel;
    [SerializeField] private Transform player1SpawnPoint;
    [SerializeField] private Transform player2SpawnPoint;
    private PlayerBuilder player1;
    private PlayerBuilder player2;
    private void Awake()
    {
        player1 = Instantiate(CharacterSelectContainer.Player1Character, player1SpawnPoint.position, player1SpawnPoint.rotation);
        player2 = Instantiate(CharacterSelectContainer.Player2Character, player2SpawnPoint.position, player2SpawnPoint.rotation);
        player2.transform.localScale = new Vector3(player2.transform.localScale.x, player2.transform.localScale.y, -player2.transform.localScale.z);
        player1.BuildPlayer(Player.PLAYER1_NUMBER);
        player2.BuildPlayer(Player.PLAYER2_NUMBER);
        player1.InitializeEnemyForPlayer(player2.transform);
        player2.InitializeEnemyForPlayer(player1.transform);
        fightSceneStatsPanel.InitializeStatPanel(player1.GetPlayer(), player2.GetPlayer());
    }
}
