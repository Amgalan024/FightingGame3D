using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterSelectMenuConfig),
        menuName = "Configs/CharacterSelectMenu/" + nameof(CharacterSelectMenuConfig))]
    public class CharacterSelectMenuConfig : ScriptableObject
    {
        [SerializeField] private AssetReference _fightScene;
        [SerializeField] private List<CharacterConfig> _characterConfigs;

        public AssetReference FightScene => _fightScene;
        public List<CharacterConfig> CharacterConfigs => _characterConfigs;
    }
}