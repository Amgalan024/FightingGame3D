using System.Linq;
using MVC.Configs.SODataModels;

namespace MVC.Models
{
    public class ComboModelsContainer
    {
        public ComboModel[] ComboModels { get; }

        public ComboModelsContainer(ComboData[] comboList, InputModelsContainer inputModelsContainer)
        {
            ComboModels = new ComboModel[comboList.Length];

            var sortedComboList = comboList.OrderByDescending(x => x.ControlNames.Length).ToList();

            for (int i = 0; i < ComboModels.Length; i++)
            {
                ComboModels[i] = new ComboModel(sortedComboList[i].Name, sortedComboList[i].Damage,
                    inputModelsContainer, sortedComboList[i]);
            }
        }
    }
}