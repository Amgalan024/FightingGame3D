using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform buttonsContainer;
    public int SelectedButton {
        set
        {
            mainMenuButtonsArray[selectedButton].DeselectButton();
            selectedButton = value;
            mainMenuButtonsArray[selectedButton].SelectButton();
        }
        get
        {
            return selectedButton;
        }
    }
    private MainMenuButton[] mainMenuButtonsArray;
    private int selectedButton;

    private void Start()
    {
        mainMenuButtonsArray = new MainMenuButton[buttonsContainer.childCount];
        mainMenuButtonsArray = buttonsContainer.GetComponentsInChildren<MainMenuButton>();
        mainMenuButtonsArray[SelectedButton].SelectButton();
    }
    private void Update()
    {
        SelectMenuButtonInput();
    }
    private void SelectMenuButtonInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && SelectedButton > 0)
        {
            SelectedButton--;
        }
        if (Input.GetKeyDown(KeyCode.S) && SelectedButton < mainMenuButtonsArray.Length - 1)
        {
            SelectedButton++;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainMenuButtonsArray[SelectedButton].LoadScene();
        }
    }

}
