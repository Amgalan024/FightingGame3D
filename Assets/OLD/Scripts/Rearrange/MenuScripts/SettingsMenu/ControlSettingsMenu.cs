using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

//Переделать меню настроек контролов ,два столбца леблы названия кнопок справла - слева считываение кнопки с клавиши
//, внизу отдельная кнопка для сохранения результата
class ControlSettingsMenu : MonoBehaviour
{
    //path to saved file = C:\Users\tokur\AppData\LocalLow\DefaultCompany\FightingTest

    [SerializeField] private GameObject[] player1ButtonsArray;
    [SerializeField] private GameObject[] player2ButtonsArray;

    [SerializeField] private GameObject player1MenuController;
    [SerializeField] private GameObject player2MenuController;

    private Text[] player1ButtonsTextArray;
    private Text[] player2ButtonsTextArray;

    public static KeyCode[] Player1SettingsArray;
    public static KeyCode[] Player2SettingsArray;

    private bool isChangingButton = false;


    private int selectedPlayer1ButtonIdex;
    private int selectedPlayer2ButtonIdex;


    public static List<KeyCode[]> SettingsData;


    private int menuSelector = 1;

    private void Start()
    {
        //создать общий класс Menu  
        //player1MenuController.GetComponent<MainMenu>().OnButtonSelected += ControlSettingsMenu_OnPlayer1ButtonSelected;
        //player2MenuController.GetComponent<MainMenu>().OnButtonSelected += ControlSettingsMenu_OnPlayer2ButtonSelected;
        int arrayLenght = player1ButtonsArray.Length;

        Player1SettingsArray = new KeyCode[arrayLenght];
        Player2SettingsArray = new KeyCode[arrayLenght];
        player1ButtonsTextArray = new Text[arrayLenght];
        player2ButtonsTextArray = new Text[arrayLenght];

        if (LoadSettingsFromFile() != null)
        {
            SettingsData = LoadSettingsFromFile();
            Player1SettingsArray = SettingsData[0];
            Player2SettingsArray = SettingsData[1];
        }

        for (int i = 0; i < arrayLenght; i++)
        {
            player1ButtonsTextArray[i] = player1ButtonsArray[i].GetComponent<Text>();
            player1ButtonsTextArray[i].text = Player1SettingsArray[i].ToString();
            player2ButtonsTextArray[i] = player2ButtonsArray[i].GetComponent<Text>();
            player2ButtonsTextArray[i].text = Player2SettingsArray[i].ToString();
        }
    }

    private void ControlSettingsMenu_OnPlayer2ButtonSelected(int index)
    {
        selectedPlayer2ButtonIdex = index;
    }

    private void ControlSettingsMenu_OnPlayer1ButtonSelected(int index)
    {
        selectedPlayer1ButtonIdex = index;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !isChangingButton)
        {
            if (menuSelector == 1)
            {
                Player1SettingsArray[selectedPlayer1ButtonIdex] = KeyCode.None;
                isChangingButton = true;
                player1MenuController.SetActive(false);
            }

            if (menuSelector == 2)
            {
                Player2SettingsArray[selectedPlayer2ButtonIdex] = KeyCode.None;
                isChangingButton = true;
                player2MenuController.SetActive(false);
            }
        }

        if ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && !isChangingButton)
        {
            player1MenuController.SetActive(false);
            player2MenuController.SetActive(true);
            menuSelector = 2;
        }

        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && !isChangingButton)
        {
            player1MenuController.SetActive(true);
            player2MenuController.SetActive(false);
            menuSelector = 1;
        }

        if (Input.GetKeyUp(KeyCode.KeypadEnter) && !isChangingButton)
        {
            SaveSettingsToFile();
        }
    }

    private void OnGUI()
    {
        if (isChangingButton)
        {
            if (menuSelector == 1)
            {
                Player1SaveButtonSetting(selectedPlayer1ButtonIdex);
            }

            if (menuSelector == 2)
            {
                Player2SaveButtonSetting(selectedPlayer2ButtonIdex);
            }
        }
    }

    public void Player1SaveButtonSetting(int i)
    {
        if (Input.GetKeyDown(Event.current.keyCode))
        {
            Player1SettingsArray[i] = Event.current.keyCode;
            player1ButtonsTextArray[i].text = Player1SettingsArray[i].ToString();
            StartCoroutine(ResetChangingButton(player1MenuController));
        }
    }

    public void Player2SaveButtonSetting(int i)
    {
        if (Input.GetKeyDown(Event.current.keyCode))
        {
            Player2SettingsArray[i] = Event.current.keyCode;
            player2ButtonsTextArray[i].text = Player2SettingsArray[i].ToString();
            StartCoroutine(ResetChangingButton(player2MenuController));
        }
    }

    public IEnumerator ResetChangingButton(GameObject SettingsMenu)
    {
        yield return new WaitForSeconds(0.2f);
        isChangingButton = false;
        SettingsMenu.SetActive(true);
    }

    public static void SaveSettingsToFile()
    {
        SettingsData = new List<KeyCode[]>();
        SettingsData.Add(Player1SettingsArray);
        SettingsData.Add(Player2SettingsArray);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, SettingsData);
        stream.Close();
    }

    public static List<KeyCode[]> LoadSettingsFromFile()
    {
        string path = Application.persistentDataPath + "/settings.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<KeyCode[]> SettingsData = formatter.Deserialize(stream) as List<KeyCode[]>;
            stream.Close();
            return SettingsData;
        }
        else
        {
            Debug.Log("NO FILE FOUND" + path);
            return null;
        }
    }
}