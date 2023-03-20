using UnityEngine;
using UnityEngine.UI;

namespace MVC.Menu.Views
{
    public class CharacterSelectButtonView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _icon;

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SelectButton(int playerNumber)
        {
            _animator.SetBool("IsSelectedByPlayer" + playerNumber, true);
        }

        public void CommitSelect(int playerNumber)
        {
            _animator.SetBool("SelectCommittedByPlayer" + playerNumber, true);
        }

        public void UnselectButton(int playerNumber)
        {
            _animator.SetBool("IsSelectedByPlayer" + playerNumber, false);
        }
    }
}