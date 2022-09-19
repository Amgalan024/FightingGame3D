using MVC.Configs;
using MVC.Gameplay.Models.Player;
using MVC.Menu.Models;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Gameplay.Services
{
    public class FightSceneFactory
    {
        private readonly GameplayVisualConfig _visualConfig;
        private readonly FightSceneStorage _storage;
        private readonly SelectedCharactersContainer _charactersContainer;

        private readonly Transform _parent;

        public FightSceneFactory(FightSceneStorage storage, GameplayVisualConfig visualConfig,
            SelectedCharactersContainer charactersContainer, LifetimeScope lifetimeScope)
        {
            _storage = storage;
            _visualConfig = visualConfig;
            _charactersContainer = charactersContainer;
            _parent = lifetimeScope.transform;
        }

        public void CreateFightLocation()
        {
            var fightLocation = Object.Instantiate(_visualConfig.FightLocationView, _parent);

            _storage.FightLocationView = fightLocation;
        }

        public void CreatePlayers()
        {
            for (int i = 0; i < _charactersContainer.PlayerConfigs.Count; i++)
            {
                var playerModel = new PlayerModel(i, _charactersContainer.PlayerConfigs[i]);

                var playerView = Object.Instantiate(_charactersContainer.PlayerConfigs[i].Prefab,
                    _storage.FightLocationView.PlayerSpawnPoints[i]);

                var comboConfig = _charactersContainer.PlayerConfigs[i].ComboConfig;

                _storage.PlayerModels.Add(playerModel);
                _storage.PlayerViews.Add(playerView);

                _storage.PlayerModelsByView.Add(playerView, playerModel);
                _storage.PlayerViewsByModel.Add(playerModel, playerView);
                _storage.ComboConfigsByModel.Add(playerModel, comboConfig);

                var playerAttackModel = new PlayerAttackModel();

                _storage.PlayerAttackModelsByView.Add(playerView.AttackHitBoxView, playerAttackModel);
            }
        }
    }
}