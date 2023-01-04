using System.Linq;
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
                var characterPrefab = _charactersContainer.PlayerConfigs[i].Prefab;
                var rotation = characterPrefab.transform.rotation;
                var position = _storage.FightLocationView.PlayerSpawnPoints[i].position;

                var playerView = Object.Instantiate(characterPrefab, position, rotation);

                var playerModel = new PlayerModel(i, _charactersContainer.PlayerConfigs[i]);

                var characterConfig = _charactersContainer.PlayerConfigs[i];

                _storage.PlayerModels.Add(playerModel);
                _storage.PlayerViews.Add(playerView);

                _storage.PlayerModelsByView.Add(playerView, playerModel);
                _storage.PlayerViewsByModel.Add(playerModel, playerView);
                _storage.CharacterConfigsByModel.Add(playerModel, characterConfig);

                var playerAttackModel = new PlayerAttackModel();

                _storage.AttackModelsByPlayer.Add(playerModel, playerAttackModel);
                _storage.AttackModelsByView.Add(playerView.AttackHitBoxView, playerAttackModel);

                //
                var playerContainer = new PlayerContainer(playerModel, playerView, playerAttackModel,
                    playerView.AttackHitBoxView);

                _storage.PlayerContainers.Add(playerContainer);
                //
            }

            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var opponentContainer = _storage.PlayerContainers.FirstOrDefault(c => c != playerContainer);
                _storage.OpponentContainerByPlayer.Add(playerContainer, opponentContainer);
            }
        }
    }
}