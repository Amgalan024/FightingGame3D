using UnityEngine;

namespace MVC.Configs
{
    public class CharacterSelectMenuInputConfig : ScriptableObject
    {
        [SerializeField] private KeyCode _up;
        [SerializeField] private KeyCode _down;
        [SerializeField] private KeyCode _right;
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _choose;

        public KeyCode Up => _up;
        public KeyCode Down => _down;
        public KeyCode Right => _right;
        public KeyCode Left => _left;
        public KeyCode Choose => _choose;
    }
}