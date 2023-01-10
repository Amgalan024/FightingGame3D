using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MVC_Pattern.Scripts.Services.SceneLoader.Config
{
    [CreateAssetMenu(fileName = nameof(EndGameConfig), menuName = "Configs/Endgame/" + nameof(EndGameConfig))]
    public class EndGameConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _mainMenuScene;
        [SerializeField] private AssetReference _fightScene;
        [SerializeField] private AssetReference _characterSelectScene;

        public AssetReference MainMenuScene => _mainMenuScene;
        public AssetReference FightScene => _fightScene;
        public AssetReference CharacterSelectScene => _characterSelectScene;
    }
}