using UnityEngine;
using UnityEngine.UI;

namespace MVC_Pattern.Scripts.MainMenu.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playerVsPlayer;
        [SerializeField] private Button _playerVsPlayerNetwork;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _exit;

        public Button PlayerVsPlayer => _playerVsPlayer;
        public Button PlayerVsPlayerNetwork => _playerVsPlayerNetwork;
        public Button Settings => _settings;
        public Button Exit => _exit;
    }
}