using UnityEngine;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterSelectMenuGameplayConfig),
        menuName = "Configs/CharacterSelectMenu/" + nameof(CharacterSelectMenuGameplayConfig))]
    public class CharacterSelectMenuGameplayConfig : ScriptableObject
    {
        [SerializeField] private CharacterConfig[] _characterConfigs;

        public CharacterConfig[] CharacterConfigs => _characterConfigs;
    }
}