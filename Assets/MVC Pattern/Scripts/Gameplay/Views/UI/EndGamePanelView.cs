using UnityEngine;
using UnityEngine.UI;

namespace MVC.Gameplay.Views.UI
{
    public class EndGamePanelView : MonoBehaviour
    {
        [SerializeField] private Button _backToMenu;
        [SerializeField] private Button _restart;

        public Button BackToMenu => _backToMenu;
        public Button Restart => _restart;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}