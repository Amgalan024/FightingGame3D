using System;
using UnityEngine;

namespace MVC.Configs.SODataModels
{
    [Serializable]
    public class PhaseAttackData
    {
        [SerializeField] private string _attackName;
        [SerializeField] private float[] _phaseAttackDamage;

        public string AttackName => _attackName;
        public float[] PhaseAttackDamage => _phaseAttackDamage;
    }
}