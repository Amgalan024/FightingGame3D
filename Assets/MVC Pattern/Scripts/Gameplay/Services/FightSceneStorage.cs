using System.Collections.Generic;
using System.Linq;
using MVC.Gameplay.Models.Player;
using MVC.Gameplay.Views;
using MVC.Views;

namespace MVC.Gameplay.Services
{
    public class FightSceneStorage
    {
        public FightLocationView FightLocationView { get; set; }

        public List<PlayerContainer> PlayerContainers { get; } = new List<PlayerContainer>();

        /// <summary>
        /// todo: На каждый коллайдер атаки(может быть проджектайл) делать свою аттак модель, и по коллайдеру брать привязанную модель и наносить урон. Так же сделать привязку атак модели к плеер модели что бы понимать кто атакует
        /// </summary>
        public Dictionary<TriggerDetectorView, PlayerAttackModel> AttackModelsByView { get; } =
            new Dictionary<TriggerDetectorView, PlayerAttackModel>(2);

        public PlayerContainer GetContainerByModel(PlayerModel playerModel)
        {
            return PlayerContainers.First(c => c.Model == playerModel);
        }
    }
}