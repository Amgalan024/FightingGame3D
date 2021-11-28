using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraAnimation : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCinemachineCamera;
    [SerializeField] private CameraMovement cameraBorderScript;
    [SerializeField] private GameObject sideBorders;
    [SerializeField] private float animSpeed;
    private Animator mainCameraAnim;
    private Camera mainCamera;
    private bool isClosedSize = true;
    private bool isAnimPlaying = false;
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        mainCameraAnim = GetComponent<Animator>();
        cameraBorderScript.GetComponent<CameraMovement>().OnPlayersEnteredClosedRange += OnPlayersEnteredClosedRange;
        cameraBorderScript.GetComponent<CameraMovement>().OnPlayersExitClosedRange += OnPlayersExitClosedRange;
    }
    private void FixedUpdate()
    {
        SideBordersUpdate();
    }
    private void OnPlayersExitClosedRange()
    {
        StartCoroutine(DistanceCamera());
    }
    private void OnPlayersEnteredClosedRange()
    {
        StartCoroutine(CloseCamera());
    }
    public void SideBordersUpdate()
    {
        if (!isClosedSize && !sideBorders.activeInHierarchy)
        {
            sideBorders.SetActive(true);
            sideBorders.transform.position = new Vector3(mainCamera.transform.position.x, sideBorders.transform.position.y, sideBorders.transform.position.z);
        }
        if (isClosedSize && sideBorders.activeInHierarchy)
        {
            sideBorders.SetActive(false);
        }
        if (sideBorders.activeInHierarchy)
        {
            sideBorders.transform.position = new Vector3(mainCamera.transform.position.x, sideBorders.transform.position.y, sideBorders.transform.position.z);
        }
    }
    private IEnumerator CloseCamera()
    {
        while (mainCinemachineCamera.m_Lens.FieldOfView >= 20)
        {
            mainCinemachineCamera.m_Lens.FieldOfView -= Time.deltaTime * animSpeed;
            yield return null;
        }
        isClosedSize = true;
        yield return null;
    }
    private IEnumerator DistanceCamera()
    {
        while (mainCinemachineCamera.m_Lens.FieldOfView <= 30)
        {
            mainCinemachineCamera.m_Lens.FieldOfView += Time.fixedDeltaTime * animSpeed; 
            yield return new WaitForFixedUpdate();
        }
        isClosedSize = false;
        yield return new WaitForFixedUpdate();
    }
}
