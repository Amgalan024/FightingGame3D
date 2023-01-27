using UnityEngine;

namespace MVC.Menu.Views
{
    public class CharacterSelectButtonView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void PlaySelectedByPlayerAnimation(int playerNumber)
        {
            _animator.SetBool("IsSelectedByPlayer" + playerNumber, true);
        }

        public void PlayUnselectedByPlayerAnimation(int playerNumber)
        {
            _animator.SetBool("IsSelectedByPlayer" + playerNumber, false);
        }
    }
}