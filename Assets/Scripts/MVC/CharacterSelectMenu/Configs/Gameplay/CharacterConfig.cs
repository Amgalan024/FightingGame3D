﻿using MVC.Views;
using UnityEngine;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig),
        menuName = "Configs/CharacterSelectMenu/" + nameof(CharacterConfig))]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _maxHealthPoints;
        [SerializeField] private int _maxEnergyPoints;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private int _punchDamage;
        [SerializeField] private int _kickDamage;
        [SerializeField] private PlayerView _prefab;
        [SerializeField] private ComboConfig _comboConfig;
        public Sprite Icon => _icon;
        public int MaxHealthPoints => _maxHealthPoints;
        public int MaxEnergyPoints => _maxEnergyPoints;
        public float MovementSpeed => _movementSpeed;
        public float JumpForce => _jumpForce;
        public int PunchDamage => _punchDamage;
        public int KickDamage => _kickDamage;
        public PlayerView Prefab => _prefab;
        public ComboConfig ComboConfig => _comboConfig;
    }
}