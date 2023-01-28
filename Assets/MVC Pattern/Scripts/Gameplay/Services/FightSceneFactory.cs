using System.Linq;
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
        private readonly SelectedCharactersContainer _selectedCharactersContainer;
        private readonly PlayerInputConfig[] _inputConfigs;

        private readonly Transform _parent;

        public FightSceneFactory(FightSceneStorage storage, GameplayVisualConfig visualConfig,
            SelectedCharactersContainer selectedCharactersContainer, LifetimeScope lifetimeScope,
            PlayerInputConfig[] inputConfigs)
        {
            _storage = storage;
            _visualConfig = visualConfig;
            _selectedCharactersContainer = selectedCharactersContainer;
            _inputConfigs = inputConfigs;
            _parent = lifetimeScope.transform;
        }

        public void CreateFightLocation()
        {
            var random = Random.Range(0, _visualConfig.FightLocationViews.Length);

            var fightLocation = Object.Instantiate(_visualConfig.FightLocationViews[random], _parent);

            _storage.FightLocationView = fightLocation;
        }

        public void CreatePlayers()
        {
            foreach (var characterConfigByPlayer in _selectedCharactersContainer.CharacterConfigsByPlayer)
            {
                var playerIndex = characterConfigByPlayer.Key;
                var characterConfig = characterConfigByPlayer.Value;

                var playerModel = new PlayerModel(playerIndex, characterConfig);

                var playerView = CreatePlayerView(characterConfig,
                    _storage.FightLocationView.PlayerSpawnPoints[playerIndex]);

                var inputModelsContainer = new InputFilterModelsContainer(_inputConfigs[playerIndex].InputModels);

                var comboList = characterConfig.ComboConfig.ComboList;
                var comboModelsContainer = new ComboModelsContainer(comboList, inputModelsContainer);

                var playerAttackModel = new PlayerAttackModel();

                var animationData = characterConfig.PlayerAnimationData;

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