using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MVC_Pattern.Scripts.Startup
{
    [CreateAssetMenu(fileName = nameof(StartupConfig), menuName = "Configs/Startup/" + nameof(StartupConfig))]
    public class StartupConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _servicesScene;
        [SerializeField] private AssetReference _mainMenuScene;

        public AssetReference ServicesScene => _servicesScene;
        public AssetReference MainMenuScene => _mainMenuScene;
    }
}