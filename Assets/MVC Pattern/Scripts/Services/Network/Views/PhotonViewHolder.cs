using Photon.Pun;
using UnityEngine;

namespace MVC.Menu.Views.Network
{
    public class PhotonViewHolder : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;

        public PhotonView PhotonView => _photonView;
    }
}