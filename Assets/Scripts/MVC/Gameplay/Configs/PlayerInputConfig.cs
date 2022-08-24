using System.Collections.Generic;
using MVC.Models;
using UnityEngine;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(PlayerInputConfig),
        menuName = "Configs/Gameplay/" + nameof(PlayerInputConfig))]
    public class PlayerInputConfig : ScriptableObject
    {
        [SerializeField] private List<InputModel> _inputModels;

        public List<InputModel> InputModels => _inputModels;
    }
}