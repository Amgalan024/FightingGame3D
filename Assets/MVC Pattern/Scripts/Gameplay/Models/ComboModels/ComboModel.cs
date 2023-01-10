using MVC.Configs.SODataModels;

namespace MVC.Models
{
    public class ComboModel
    {
        public InputFilterModel[] InputModels { get; }
        public string Name { get; }
        public int Damage { get; }
        public int ComboCount { get; set; }

        public ComboModel(string name, int damage, InputFilterModelsContainer inputFilterModelsContainer, ComboData comboData)
        {
            Name = name;
            Damage = damage;

            InputModels = new InputFilterModel[comboData.ControlNames.Length];

            for (int i = 0; i < InputModels.Length; i++)
            {
                InputModels[i] = inputFilterModelsContainer.InputFilterModelsByType[comboData.ControlNames[i]];
            }
        }
    }
}