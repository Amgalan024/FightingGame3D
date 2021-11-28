using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraMovement : MonoBehaviour
{
    public delegate void CameraEventHandler();
    public event CameraEventHandler OnPlayersEnteredClosedRange;
    public event CameraEventHandler OnPlayersExitClosedRange;

    [SerializeField] private CinemachineVirtualCamera mainCinemachineCamera;
    [SerializeField] private float yOffset;
    public float maxClosedRangeDistanceBetweenPlayers { set; get; }
    public float maxDistancedRangeDistanceBetweenPlayers { set; get; }

    private GameObject player1;
    private GameObject player2;
    private GameObject mainCameraTarget;
    private void Start()
    {
        mainCameraTarget = new GameObject();
        mainCinemachineCamera.m_Follow = mainCameraTarget.transform;
        maxClosedRangeDistanceBetweenPlayers = 6;
        maxDistancedRangeDistanceBetweenPlayers = 10;
    }

    private void FixedUpdate()
    {
        if (player1 != null && player2 != null)
        {
            mainCameraTarget.transform.position = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2, (player1.transform.position.y + player2.transform.position.y)/2 + yOffset, player1.transform.position.z);
            HandleCameraSizeEvents();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Distance = " + Math.Abs(player1.transform.position.x - player2.transform.position.x));
            Debug.Log("Distance sqrMag = " + (new Vector2(player1.transform.position.x, 0f) - new Vector2(player2.transform.position.x, 0f)).sqrMagnitude);
        }
    }

    public void HandleCameraSizeEvents()
    {
        float distanceBetweenPlayers = Math.Abs(player1.transform.position.x - player2.transform.position.x);
        if (distanceBetweenPlayers > maxClosedRangeDistanceBetweenPlayers && mainCinemachineCamera.m_Lens.FieldOfView <= 20)
        {
            OnPlayersExitClosedRange?.Invoke();
            Debug.Log("PlayDistanceCamera");
        }
        if (distanceBetweenPlayers < maxClosedRangeDistanceBetweenPlayers && mainCinemachineCamera.m_Lens.FieldOfView >= 30) 
        {
            OnPlayersEnteredClosedRange?.Invoke();
            Debug.Log("PlayCloseCamera");
        }
    }
    public void InitializeCameraMovement(GameObject Player1, GameObject Player2)
    {
        player1 = Player1;
        player2 = Player2;
    }
}
