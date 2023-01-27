using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC_Pattern.Scripts.NetworkMenu.Views
{
    public class NetworkMenuView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _createRoomText;
        [SerializeField] private Button _createRoomButton;

        [SerializeField] private TextMeshProUGUI _joinRoomText;
        [SerializeField] private Button _joinRoomButton;

        public TextMeshProUGUI CreateRoomText => _createRoomText;
        public Button CreateRoomButton => _createRoomButton;

        public TextMeshProUGUI JoinRoomText => _joinRoomText;
        public Button JoinRoomButton => _joinRoomButton;
    }
}