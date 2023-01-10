using UnityEngine;
using UnityEngine.UI;

namespace MVC.Gameplay.Views.UI
{
    public class EndGamePanelView : MonoBehaviour
    {
        [SerializeField] private Button _backToMenu;

        public Button BackToMenu => _backToMenu;

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