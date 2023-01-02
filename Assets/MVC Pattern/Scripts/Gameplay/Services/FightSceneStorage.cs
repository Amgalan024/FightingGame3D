using System.Collections.Generic;
using System.Linq;
using MVC.Configs;
using MVC.Gameplay.Models.Player;
using MVC.Gameplay.Views;
using MVC.Views;

namespace MVC.Gameplay.Services
{
    public class FightSceneStorage
    {
        public FightLocationView FightLocationView { get; set; }

        public List<PlayerView> PlayerViews { get; } = new List<PlayerView>(2);

        public List<PlayerModel> PlayerModels { get; } = new List<PlayerModel>(2);

        public Dictionary<PlayerModel, CharacterConfig> CharacterConfigsByModel { get; } =
            new Dictionary<PlayerModel, CharacterConfig>(2);

        public Dictionary<PlayerModel, PlayerView> PlayerViewsByModel { get; } =
            new Dictionary<PlayerModel, PlayerView>(2);

        public Dictionary<PlayerView, PlayerModel> PlayerModelsByView { get; } =
            new Dictionary<PlayerView, PlayerModel>(2);

        public Dictionary<TriggerDetectorView, PlayerAttackModel> PlayerAttackModelsByView { get; } =
            new Dictionary<TriggerDetectorView, PlayerAttackModel>(2);

        public PlayerView GetOpponentViewByModel(PlayerModel playerModel)
        {
            return PlayerViewsByModel.FirstOrDefault(p => p.Key != playerModel).Value;
        }

        public PlayerModel GetOpponentModelByModel(PlayerModel playerModel)
        {
            return PlayerViewsByModel.FirstOrDefault(p => p.Key != playerModel).Key;
        }

        public TriggerDetectorView GetOpponentAttackViewByModel(PlayerAttackModel playerAttackModel)
        {
            return PlayerAttackModelsByView.FirstOrDefault(p => p.Value != playerAttackModel).Key;
        }
    }
}