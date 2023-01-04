using MVC.Configs.SODataModels;
using UnityEngine;

namespace MVC.Configs
{
    [CreateAssetMenu(fileName = nameof(ComboConfig), menuName = "Configs/Gameplay/" + nameof(ComboConfig))]
    public class ComboConfig : ScriptableObject
    {
        [SerializeField] private ComboData[] _comboList;

        public ComboData[] ComboList => _comboList;
    }
}