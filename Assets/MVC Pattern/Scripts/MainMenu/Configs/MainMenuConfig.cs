using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MVC.Menu.Configs
{
    [CreateAssetMenu(fileName = nameof(MainMenuConfig), menuName = "Configs/MainMenu/" + nameof(MainMenuConfig))]
    public class MainMenuConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _characterSelectScene;
        [SerializeField] private AssetReference _networkScene;

        public AssetReference CharacterSelectScene => _characterSelectScene;
        public AssetReference NetworkScene => _networkScene;
    }
}