using MVC.Configs;
using MVC.Gameplay.Models.Player;
using MVC.Menu.Models;
using UnityEngine;

namespace MVC.Gameplay.Services
{
    public class FightSceneFactory
    {
        private readonly GameplayVisualConfig _visualConfig;
        private readonly FightSceneStorage _storage;
        private readonly SelectedCharactersContainer _charactersContainer;

        public FightSceneFactory(FightSceneStorage storage, GameplayVisualConfig visualConfig,
            SelectedCharactersContainer charactersContainer)
        {
            _storage = storage;
            _visualConfig = visualConfig;
            _charactersContainer = charactersContainer;
        }

        public void CreateFightLocation()
        {
            var fightLocation = Object.Instantiate(_visualConfig.FightLocationView);

            _storage.FightLocationView = fightLocation;
        }

        public void CreatePlayers()
        {
            for (int i = 0; i < _charactersContainer.PlayerConfigs.Count; i++)
            {
                var playerModel = new PlayerModel(i, _charactersContainer.PlayerConfigs[i]);
                var playerView = Object.Instantiate(_charactersContainer.PlayerConfigs[i].Prefab,
                    _storage.FightLocationView.PlayerSpawnPoints[i]);

                _storage.PlayerModels.Add(playerModel);
                _storage.PlayerViews.Add(playerView);

                _storage.PlayerModelsByView.Add(playerView, playerModel);
                _storage.PlayerViewsByModel.Add(playerModel, playerView);

                var playerAttackModel = new PlayerAttackModel();

                _storage.PlayerAttackModelsByView.Add(playerView.AttackHitBoxView, playerAttackModel);
            }
        }
    }
}