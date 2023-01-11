using System.Linq;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Utils.Disposable;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class FightController : DisposableWithCts, IStartable
    {
        private readonly FightSceneFactory _factory;
        private readonly FightSceneStorage _storage;
        private readonly LifetimeScopeFactory _lifetimeScopeFactory;

        private readonly FightSceneModel _fightSceneModel;
        private readonly PlayerHUDView[] _playersStatsPanels;

        public FightController(FightSceneFactory factory, FightSceneStorage storage,
            LifetimeScopeFactory lifetimeScopeFactory, FightSceneModel fightSceneModel,
            PlayerHUDView[] playersStatsPanels)
        {
            _factory = factory;
            _storage = storage;
            _lifetimeScopeFactory = lifetimeScopeFactory;
            _fightSceneModel = fightSceneModel;
            _playersStatsPanels = playersStatsPanels;
        }

        void IStartable.Start()
        {
            _fightSceneModel.OnRoundEnded += RestartFight;

            _factory.CreateFightLocation();
            _factory.CreatePlayers();

            InitializeOpponentsForPlayers();

            CreatePlayerLifetimeScopes();
        }

        private void RestartFight()
        {
            foreach (var playerLifetimeScope in _fightSceneModel.PlayerLifetimeScopes)
            {
                playerLifetimeScope.Dispose();
            }

            _fightSceneModel.CameraControllerScope.Dispose();

            _fightSceneModel.PlayerLifetimeScopes.Clear();
            _storage.PlayerContainers.Clear();
            _storage.AttackModelsByView.Clear();

            _factory.CreatePlayers();

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
            _fightSceneModel.CameraControllerScope = _lifetimeScopeFactory.CreateCameraControllerScope();

            foreach (var playerContainer in _storage.PlayerContainers)
            {
                var playerIndex = _storage.PlayerContainers.IndexOf(playerContainer);
                var playersStatsPanel = _playersStatsPanels[playerIndex];

                var playerLifetimeScope =
                    _lifetimeScopeFactory.CreatePlayerLifetimeScope(playerContainer, playersStatsPanel);

                _fightSceneModel.PlayerLifetimeScopes.Add(playerLifetimeScope);
            }
        }
    }
}