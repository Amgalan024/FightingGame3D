using System.Collections.Generic;
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

        public Dictionary<PlayerModel, ComboConfig> ComboConfigsByModel { get; } =
            new Dictionary<PlayerModel, ComboConfig>(5);

        public Dictionary<PlayerModel, PlayerView> PlayerViewsByModel { get; } =
            new Dictionary<PlayerModel, PlayerView>(5);

        public Dictionary<PlayerView, PlayerModel> PlayerModelsByView { get; } =
            new Dictionary<PlayerView, PlayerModel>(5);
        
        public Dictionary<PlayerAttackHitBoxView, PlayerAttackModel> PlayerAttackModelsByView { get; } =
            new Dictionary<PlayerAttackHitBoxView, PlayerAttackModel>();
    }
}