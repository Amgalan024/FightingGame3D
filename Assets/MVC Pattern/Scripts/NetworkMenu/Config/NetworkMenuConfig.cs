using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MVC_Pattern.Scripts.NetworkMenu.Config
{
    [CreateAssetMenu(fileName = nameof(NetworkMenuConfig),
        menuName = "Configs/NetworkMenu/" + nameof(NetworkMenuConfig))]
    public class NetworkMenuConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _characterSelectScene;

        public AssetReference CharacterSelectScene => _characterSelectScene;
    }
}