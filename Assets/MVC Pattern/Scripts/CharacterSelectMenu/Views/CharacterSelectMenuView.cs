using UnityEngine;
using UnityEngine.UI;

namespace MVC.Menu.Views
{
    public class CharacterSelectMenuView : MonoBehaviour
    {
        [SerializeField] private Transform _buttonsContainer;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        public Transform ButtonsContainer => _buttonsContainer;
        public GridLayoutGroup GridLayoutGroup => _gridLayoutGroup;
    }
}