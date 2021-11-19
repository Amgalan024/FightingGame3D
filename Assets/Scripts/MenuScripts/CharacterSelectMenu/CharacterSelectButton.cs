using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CharacterSelectButton : MonoBehaviour
{
    [SerializeField] private PlayerBuilder character;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SelectButton(int playerNumber)
    {
        animator.SetBool("IsSelectedByPlayer" + playerNumber.ToString(), true);
    }
    public void UnselectButton(int playerNumber)
    {
        animator.SetBool("IsSelectedByPlayer" + playerNumber.ToString(), false);
    }
    public void SelectCharacter(int playerNumber)
    {
        if (playerNumber == 1)
        {
            CharacterSelectContainer.Player1Character = character;
            Debug.Log("Player1 Character Selected");
        }
        else if (playerNumber == 2)
        {
            CharacterSelectContainer.Player2Character = character;
            Debug.Log("Player2 Character Selected");
        }
    }
}
