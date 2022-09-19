using UnityEngine;

public class PlayerColliderInteractions : MonoBehaviour, IPlayerComponent
{
    public PlayerModel PlayerModel { private set; get; }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlatformView>())
        {
            PlayerModel.IsGrounded.Value = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlatformView>())
        {
            PlayerModel.IsGrounded.Value = true;
        }
    }

    public void InitializeComponent(PlayerModel playerModel)
    {
        PlayerModel = playerModel;
    }
}