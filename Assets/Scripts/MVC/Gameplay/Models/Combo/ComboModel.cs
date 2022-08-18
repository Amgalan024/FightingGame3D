using System.Linq;
using MVC.Configs.SODataModels;

namespace MVC.Models
{
    public class ComboModel
    {
        public ControlModel[] PlayerControlModels { get;}
        public string Name { get; }
        public int Damage { get; }
        public int ComboCount { get; set; }

        public ComboModel(string name, int damage, ControlModelsContainer controlModelsContainer, ComboData comboData)
        {
            Name = name;
            Damage = damage;

            PlayerControlModels = new ControlModel[comboData.ControlNames.Length];

            for (int i = 0; i < PlayerControlModels.Length; i++)
            {
                PlayerControlModels[i] = controlModelsContainer.ControlModels
                    .First(x => x.Name == comboData.ControlNames[i]);
            }
        }
    }
}