using System.Collections.Generic;
using UnityEngine;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterSelectMenuGameplayConfig),
        menuName = "Configs/CharacterSelectMenu/" + nameof(CharacterSelectMenuGameplayConfig))]
    public class CharacterSelectMenuGameplayConfig : ScriptableObject
    {
        [SerializeField] private List<CharacterConfig> _characterConfigs;

        public List<CharacterConfig> CharacterConfigs => _characterConfigs;
    }
}