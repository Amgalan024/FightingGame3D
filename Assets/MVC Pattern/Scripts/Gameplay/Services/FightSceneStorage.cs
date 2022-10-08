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

        public List<PlayerView> PlayerViews { get; } = new List<PlayerView>(5);

        public List<PlayerModel> PlayerModels { get; } = new List<PlayerModel>(5);

        public Dictionary<PlayerModel, CharacterConfig> CharacterConfigsByModel { get; } =
            new Dictionary<PlayerModel, CharacterConfig>(5);

        public Dictionary<PlayerModel, PlayerView> PlayerViewsByModel { get; } =
            new Dictionary<PlayerModel, PlayerView>(5);

        public Dictionary<PlayerView, PlayerModel> PlayerModelsByView { get; } =
            new Dictionary<PlayerView, PlayerModel>(5);

        public Dictionary<PlayerAttackHitBoxView, PlayerAttackModel> PlayerAttackModelsByView { get; } =
            new Dictionary<PlayerAttackHitBoxView, PlayerAttackModel>();

        public PlayerView GetOpponentViewByModel(PlayerModel playerModel)
        {
            return PlayerViewsByModel.FirstOrDefault(p => p.Key != playerModel).Value;
        }

        public PlayerModel GetOpponentModelByModel(PlayerModel playerModel)
        {
            return PlayerViewsByModel.FirstOrDefault(p => p.Key != playerModel).Key;
        }
    }
}