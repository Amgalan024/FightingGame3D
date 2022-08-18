using UnityEngine;

namespace MVC.Gameplay.Views
{
    public class FightLocationView : MonoBehaviour
    {
        [SerializeField] private Transform[] _playerSpawnPoints;

        public Transform[] PlayerSpawnPoints => _playerSpawnPoints;
    }
}