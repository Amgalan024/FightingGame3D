using UnityEngine;

namespace MVC_Pattern.Scripts.SettingsMenu.Configs
{
    [CreateAssetMenu(fileName = nameof(FightSettings), menuName = "Configs/Settings/" + nameof(FightSettings))]
    public class FightSettings : ScriptableObject
    {
        [SerializeField] private int _maxRounds;

        public int MaxRounds => _maxRounds;

        public void SetMaxRounds(int rounds)
        {
            _maxRounds = rounds;
        }
    }
}