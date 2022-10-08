using System.Linq;
using MVC.Configs;

namespace MVC.Models
{
    public class ComboModelsContainer
    {
        public ComboModel[] ComboModels { get; }

        public ComboModelsContainer(CharacterConfig characterConfig, InputModelsContainer inputModelsContainer)
        {
            ComboModels = new ComboModel[characterConfig.ComboConfig.ComboList.Length];

            var sortedComboList = characterConfig.ComboConfig.ComboList.OrderByDescending(x => x.ControlNames.Length).ToList();

            for (int i = 0; i < ComboModels.Length; i++)
            {
                ComboModels[i] = new ComboModel(sortedComboList[i].Name, sortedComboList[i].Damage,
                    inputModelsContainer, sortedComboList[i]);
            }
        }
    }
}