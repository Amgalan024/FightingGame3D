using MVC.Configs.SODataModels;
using UnityEngine;

namespace MVC.Configs
{
    public class ComboConfig : ScriptableObject
    {
        [SerializeField] private ComboData[] _comboList;

        public ComboData[] ComboList => _comboList;
    }
}