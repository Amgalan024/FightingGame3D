namespace MVC.Gameplay.Models.Player
{
    public class PlayerAttackModel
    {
        public int Damage { get; set; }
        public int StunType; //todo:добавить типы станов и в стан стейт передавать тип стана и играть соответствующую анимацию
    }
}