using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectMenu : MonoBehaviour
{
    [SerializeField] private float countDownTime;
    [SerializeField] private Text countDownText;
    [SerializeField] private Transform buttonsContainer;

    public int Player1SelectedButtonIndex
    {
        set
        {
            characterSelectButtonArray[player1SelectedButtonIndex].UnselectButton(PlayerModel.PLAYER1_NUMBER);
            player1SelectedButtonIndex = value;
            characterSelectButtonArray[player1SelectedButtonIndex].SelectButton(PlayerModel.PLAYER1_NUMBER);
        }
        get { return player1SelectedButtonIndex; }
    }

    public int Player2SelectedButtonIndex
    {
        set
        {
            characterSelectButtonArray[player2SelectedButtonIndex].UnselectButton(PlayerModel.PLAYER2_NUMBER);
            player2SelectedButtonIndex = value;
            characterSelectButtonArray[player2SelectedButtonIndex].SelectButton(PlayerModel.PLAYER2_NUMBER);
        }
        get { return player2SelectedButtonIndex; }
    }

    private int player1SelectedButtonIndex;
    private int player2SelectedButtonIndex;
    private CharacterSelectButton[] characterSelectButtonArray;
    private GridLayoutGroup gridLayoutGroup;

    private void Start()
    {
        gridLayoutGroup = buttonsContainer.GetComponent<GridLayoutGroup>();
        characterSelectButtonArray = new CharacterSelectButton[buttonsContainer.childCount];
        characterSelectButtonArray = buttonsContainer.GetComponentsInChildren<CharacterSelectButton>();
        Player1SelectedButtonIndex = 0;
        Player2SelectedButtonIndex = gridLayoutGroup.constraintCount - 1;
        characterSelectButtonArray[Player1SelectedButtonIndex].SelectButton(PlayerModel.PLAYER1_NUMBER);
        characterSelectButtonArray[Player2SelectedButtonIndex].SelectButton(PlayerModel.PLAYER2_NUMBER);
    }

    private void Update()
    {
        Player1SelectCharacterInput();
        Player2SelectCharacterInput();
        StartFightScene();
    }

    private void Player1SelectCharacterInput()
    {
        if (Input.GetKeyDown(KeyCode.S) && Player1SelectedButtonIndex <
            characterSelectButtonArray.Length - gridLayoutGroup.constraintCount)
        {
            Player1SelectedButtonIndex += gridLayoutGroup.constraintCount;
        }

        if (Input.GetKeyDown(KeyCode.W) && Player1SelectedButtonIndex > gridLayoutGroup.constraintCount - 1)
        {
            Player1SelectedButtonIndex -= gridLayoutGroup.constraintCount;
        }

        if (Input.GetKeyDown(KeyCode.D) && Player1SelectedButtonIndex < characterSelectButtonArray.Length - 1)
        {
            Player1SelectedButtonIndex++;
        }

        if (Input.GetKeyDown(KeyCode.A) && Player1SelectedButtonIndex > 0)
        {
            Player1SelectedButtonIndex--;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            characterSelectButtonArray[player1SelectedButtonIndex].SelectCharacter(PlayerModel.PLAYER1_NUMBER);
        }
    }

    private void Player2SelectCharacterInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && Player2SelectedButtonIndex <
            characterSelectButtonArray.Length - gridLayoutGroup.constraintCount)
        {
            Player2SelectedButtonIndex += gridLayoutGroup.constraintCount;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && Player2SelectedButtonIndex > gridLayoutGroup.constraintCount - 1)
        {
            Player2SelectedButtonIndex -= gridLayoutGroup.constraintCount;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && Player2SelectedButtonIndex < characterSelectButtonArray.Length - 1)
        {
            Player2SelectedButtonIndex++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && Player2SelectedButtonIndex > 0)
        {
            Player2SelectedButtonIndex--;
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            characterSelectButtonArray[Player2SelectedButtonIndex].SelectCharacter(PlayerModel.PLAYER2_NUMBER);
        }
    }

    private void StartFightScene()
    {
        if (CharacterSelectContainer.Player1Character != null && CharacterSelectContainer.Player2Character != null)
        {
            countDownTime -= Time.deltaTime;
            countDownText.text = countDownTime.ToString();
            if (countDownTime <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}