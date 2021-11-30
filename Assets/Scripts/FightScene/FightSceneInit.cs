using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FightSceneInit : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private FightSceneStatsPanel fightSceneStatsPanel;
    [SerializeField] private Transform player1SpawnPoint;
    [SerializeField] private Transform player2SpawnPoint;
    private PlayerBuilder player1;
    private PlayerBuilder player2;
    private FightSceneHandler fightSceneHandler;
    private void Awake()
    {
        InitFightScene();
        fightSceneHandler = new FightSceneHandler(player1.GetPlayer(), player2.GetPlayer());
        player1.GetPlayer().OnPlayerDied += OnPlayer2WonRound;
        player2.GetPlayer().OnPlayerDied += OnPlayer1WonRound;
    }
    private void OnPlayer1WonRound()
    {
        StartCoroutine(StartNewRound());
    }
    private void OnPlayer2WonRound()
    {
        StartCoroutine(StartNewRound());
    }
    private void InitFightScene()
    {
        player1 = Instantiate(CharacterSelectContainer.Player1Character, player1SpawnPoint.position, player1SpawnPoint.rotation);
        player2 = Instantiate(CharacterSelectContainer.Player2Character, player2SpawnPoint.position, player2SpawnPoint.rotation);
        player2.transform.localScale = new Vector3(player2.transform.localScale.x, player2.transform.localScale.y, -player2.transform.localScale.z);
        player1.BuildPlayer(Player.PLAYER1_NUMBER);
        player2.BuildPlayer(Player.PLAYER2_NUMBER);
        player1.InitializeEnemyForPlayer(player2.transform);
        player2.InitializeEnemyForPlayer(player1.transform);
        fightSceneStatsPanel.InitializeStatPanel(player1.GetPlayer(), player2.GetPlayer());
        cameraMovement.InitializeCameraMovement(player1.gameObject, player2.gameObject);
    }
    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(2f);
        player1.transform.position = player1SpawnPoint.position;
        player2.transform.position = player2SpawnPoint.position;
        player1.GetPlayer().RefreshPlayer();
        player2.GetPlayer().RefreshPlayer();
    }
}
