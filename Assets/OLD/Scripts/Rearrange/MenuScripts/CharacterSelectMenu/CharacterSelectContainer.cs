using UnityEngine;

public class CharacterSelectContainer : MonoBehaviour
{
    public static PlayerBuilder Player1Character;
    public static PlayerBuilder Player2Character;
    public static CharacterSelectContainer Instance { set; get; }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}