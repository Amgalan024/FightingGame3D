using UnityEngine;

namespace MVC.Menu.Views
{
    public class CharacterSelectButtonView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SelectButton(int playerNumber)
        {
            _animator.SetBool("IsSelectedByPlayer" + playerNumber, true);
        }

        public void UnselectButton(int playerNumber)
        {
            _animator.SetBool("IsSelectedByPlayer" + playerNumber, false);
        }
    }
}