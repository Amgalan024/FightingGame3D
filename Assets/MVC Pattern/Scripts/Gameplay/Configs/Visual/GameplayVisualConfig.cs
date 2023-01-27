using MVC.Gameplay.Views;
using UnityEngine;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(GameplayVisualConfig), menuName = "Configs/Gameplay/" + nameof(GameplayVisualConfig))]
    public class GameplayVisualConfig : ScriptableObject
    {
        [SerializeField] private FightLocationView[] _fightLocationViews;
        public FightLocationView[] FightLocationViews => _fightLocationViews;
    }
}