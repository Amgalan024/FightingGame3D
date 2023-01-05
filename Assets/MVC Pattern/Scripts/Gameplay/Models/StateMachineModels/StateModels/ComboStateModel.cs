namespace MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels
{
    public class ComboStateModel
    {
        public string Name { get; private set; }
        public int Damage { get; private set; }

        public void SetData(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }
    }
}