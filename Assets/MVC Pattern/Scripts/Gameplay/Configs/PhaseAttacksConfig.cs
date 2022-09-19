using System.Collections.Generic;
using MVC.Configs.SODataModels;
using UnityEngine;

namespace MVC.Configs
{
    public class PhaseAttacksConfig : ScriptableObject
    {
        [SerializeField] private List<PhaseAttackData> _phaseAttackDamageData;

        public List<PhaseAttackData> PhaseAttackDamageData => _phaseAttackDamageData;
    }
}