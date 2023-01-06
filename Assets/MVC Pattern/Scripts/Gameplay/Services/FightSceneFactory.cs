using MVC.Configs;
using MVC.Gameplay.Models.Player;
using MVC.Menu.Models;
using MVC.Models;
using MVC.Views;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Gameplay.Services
{
    public class FightSceneFactory
    {
        private readonly GameplayVisualConfig _visualConfig;
        private readonly FightSceneStorage _storage;
        private readonly SelectedCharactersContainer _charactersContainer;
        private readonly PlayerInputConfig[] _inputConfigs;

        private readonly Transform _parent;

        public FightSceneFactory(FightSceneStorage storage, GameplayVisualConfig visualConfig,
            SelectedCharactersContainer charactersContainer, LifetimeScope lifetimeScope,
            PlayerInputConfig[] inputConfigs)
        {
            _storage = storage;
            _visualConfig = visualConfig;
            _charactersContainer = charactersContainer;
            _inputConfigs = inputConfigs;
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

                var playerView = CreatePlayerView(_charactersContainer.PlayerConfigs[i],
                    _storage.FightLocationView.PlayerSpawnPoints[i]);

                var inputModelsContainer = new InputModelsContainer(_inputConfigs[i].InputModels);

                var comboList = _charactersContainer.PlayerConfigs[i].ComboConfig.ComboList;
                var comboModelsContainer = new ComboModelsContainer(comboList, inputModelsContainer);

                var playerAttackModel = new PlayerAttackModel();

                var animationData = _charactersContainer.PlayerConfigs[i].PlayerAnimationData;

                var playerContainer = new PlayerContainer(playerModel, playerView, playerAttackModel,
                    playerView.AttackHitBoxView, animationData, inputModelsContainer, comboModelsContainer);

                _storage.PlayerContainers.Add(playerContainer);

                _storage.AttackModelsByView.Add(playerView.AttackHitBoxView, playerAttackModel);
            }
        }

        private PlayerView CreatePlayerView(CharacterConfig characterConfig, Transform spawnPoint)
        {
            var characterPrefab = characterConfig.Prefab;
            var rotation = characterPrefab.transform.rotation;
            var position = spawnPoint.position;

            return Object.Instantiate(characterPrefab, position, rotation);
        }
    }
}