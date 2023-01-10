using System.Linq;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Utils.Disposable;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class FightSceneController : DisposableWithCts, IStartable
    {
        private readonly FightSceneFactory _factory;
        private readonly FightSceneStorage _storage;
        private readonly PlayerLifetimeScopeFactory _playerLifetimeScopeFactory;

        private readonly FightSceneModel _fightSceneModel;
        private readonly PlayerStatsPanelView[] _playersStatsPanels;

        public FightSceneController(FightSceneFactory factory, FightSceneStorage storage,
            PlayerLifetimeScopeFactory playerLifetimeScopeFactory, FightSceneModel fightSceneModel,
            PlayerStatsPanelView[] playersStatsPanels)
        {
            _factory = factory;
            _storage = storage;
            _playerLifetimeScopeFactory = playerLifetimeScopeFactory;
            _fightSceneModel = fightSceneModel;
            _playersStatsPanels = playersStatsPanels;
        }

        void IStartable.Start()
        {
            _factory.CreateFightScene();

            InitializeOpponentsForPlayers();

            CreatePlayerLifetimeScopes();
        }

        private void InitializeOpponentsForPlayers()
        {
            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var opponentContainer = _storage.PlayerContainers.First(c => c != playerContainer);

                playerContainer.SetOpponent(opponentContainer);
            }
        }

        private void CreatePlayerLifetimeScopes()
        {
            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var playerIndex = _storage.PlayerContainers.IndexOf(playerContainer);
                var playersStatsPanel = _playersStatsPanels[playerIndex];

                var playerLifetimeScope =
                    _playerLifetimeScopeFactory.CreatePlayerLifetimeScope(playerContainer, playersStatsPanel);

                _fightSceneModel.PlayerLifetimeScopes.Add(playerLifetimeScope);
            }
        }
    }
}