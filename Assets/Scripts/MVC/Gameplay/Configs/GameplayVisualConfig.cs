using MVC.Gameplay.Views;
using UnityEngine;

namespace MVC.Configs
{
    public class GameplayVisualConfig : ScriptableObject
    {
        [SerializeField] private FightLocationView _fightLocationView;
        public FightLocationView FightLocationView => _fightLocationView;
    }
}