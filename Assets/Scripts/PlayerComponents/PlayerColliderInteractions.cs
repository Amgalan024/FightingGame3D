using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerColliderInteractions : MonoBehaviour, IPlayerComponent
{
    public Player Player { private set; get; }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            Player.IsGrounded = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>())
        {
            Player.IsGrounded = true;
        }
    }
    public void InitializeComponent(Player player)
    {
        this.Player = player;
    }
}
