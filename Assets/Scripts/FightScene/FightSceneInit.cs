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
    private PlayerBuilder player1Builder;
    private PlayerBuilder player2Builder;
    private FightSceneHandler fightSceneHandler;
    private void Awake()
    {
        InitFightScene();
        fightSceneHandler = new FightSceneHandler(player1Builder.Player, player2Builder.Player);
        player1Builder.Player.OnPlayerDied += OnPlayer2WonRound;
        player2Builder.Player.OnPlayerDied += OnPlayer1WonRound;
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
        player1Builder = Instantiate(CharacterSelectContainer.Player1Character, player1SpawnPoint.position, player1SpawnPoint.rotation);
        player2Builder = Instantiate(CharacterSelectContainer.Player2Character, player2SpawnPoint.position, player2SpawnPoint.rotation);
        player2Builder.transform.localScale = new Vector3(player2Builder.transform.localScale.x, player2Builder.transform.localScale.y, -player2Builder.transform.localScale.z);
        player1Builder.BuildPlayer(Player.PLAYER1_NUMBER);
        player2Builder.BuildPlayer(Player.PLAYER2_NUMBER);
        player1Builder.InitializeEnemyForPlayer(player2Builder.transform);
        player2Builder.InitializeEnemyForPlayer(player1Builder.transform);
        fightSceneStatsPanel.InitializeStatPanel(player1Builder.Player, player2Builder.Player);
        cameraMovement.InitializeCameraMovement(player1Builder.gameObject, player2Builder.gameObject);
    }
    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(2f);
        player1Builder.transform.position = player1SpawnPoint.position;
        player2Builder.transform.position = player2SpawnPoint.position;
        player1Builder.Player.RefreshPlayer();
        player2Builder.Player.RefreshPlayer();
    }
}
