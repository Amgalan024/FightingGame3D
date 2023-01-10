using System.Collections;
using UnityEngine;

public class FightSceneInit : MonoBehaviour
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private PlayerHUDView _playerHUDView;
    [SerializeField] private Transform player1SpawnPoint;
    [SerializeField] private Transform player2SpawnPoint;
    private PlayerBuilder player1Builder;
    private PlayerBuilder player2Builder;
    private FightSceneHandler fightSceneHandler;

    private void Awake()
    {
        InitFightScene();
        fightSceneHandler = new FightSceneHandler(player1Builder.PlayerModel, player2Builder.PlayerModel);
        player1Builder.PlayerModel.OnLose += OnPlayer2WonRound;
        player2Builder.PlayerModel.OnLose += OnPlayer1WonRound;
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
        player1Builder = Instantiate(CharacterSelectContainer.Player1Character, player1SpawnPoint.position,
            player1SpawnPoint.rotation);
        player2Builder = Instantiate(CharacterSelectContainer.Player2Character, player2SpawnPoint.position,
            player2SpawnPoint.rotation);
        player1Builder.BuildPlayer(PlayerModel.PLAYER1_NUMBER);
        player2Builder.BuildPlayer(PlayerModel.PLAYER2_NUMBER);
        player1Builder.InitializeEnemyForPlayer(player2Builder.transform);
        player2Builder.InitializeEnemyForPlayer(player1Builder.transform);
        cameraMovement.InitializeCameraMovement(player1Builder.gameObject, player2Builder.gameObject);
    }

    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(2f);
        player1Builder.transform.position = player1SpawnPoint.position;
        player2Builder.transform.position = player2SpawnPoint.position;
        player1Builder.PlayerModel.RefreshPlayer();
        player2Builder.PlayerModel.RefreshPlayer();
    }
}