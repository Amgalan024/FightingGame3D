using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MVC.Menu.Configs
{
    [CreateAssetMenu(fileName = nameof(MainMenuConfig), menuName = "Configs/MainMenu/" + nameof(MainMenuConfig))]
    public class MainMenuConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _characterSelectScene;

        public AssetReference CharacterSelectScene => _characterSelectScene;
    }
}