using MVC.Configs.SODataModels;

namespace MVC.Models
{
    public class ComboModel
    {
        public InputModel[] InputModels { get; }
        public string Name { get; }
        public int Damage { get; }
        public int ComboCount { get; set; }

        public ComboModel(string name, int damage, InputModelsContainer inputModelsContainer, ComboData comboData)
        {
            Name = name;
            Damage = damage;

            InputModels = new InputModel[comboData.ControlNames.Length];

            for (int i = 0; i < InputModels.Length; i++)
            {
                InputModels[i] = inputModelsContainer.InputModelsByName[comboData.ControlNames[i]];
            }
        }
    }
}