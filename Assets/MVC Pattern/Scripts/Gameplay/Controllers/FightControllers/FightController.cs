using System.Linq;
using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using MVC.Utils.Disposable;
using MVC_Pattern.Scripts.Gameplay.Services;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class FightController : DisposableWithCts, IStartable
    {
        private readonly FightSceneFactory _fightSceneFactory;
        private readonly FightSceneStorage _fightSceneStorage;

        private readonly FightScopesFactory _fightScopesFactory;
        private readonly FightScopesStorage _fightScopesStorage;

        private readonly FightSceneModel _fightSceneModel;
        private readonly PlayerHUDView[] _playerHUDs;

        public FightController(FightSceneFactory fightSceneFactory, FightSceneStorage fightSceneStorage,
            FightScopesFactory fightScopesFactory, FightSceneModel fightSceneModel,
            PlayerHUDView[] playerHUDs, FightScopesStorage fightScopesStorage)
        {
            _fightSceneFactory = fightSceneFactory;
            _fightSceneStorage = fightSceneStorage;
            _fightScopesFactory = fightScopesFactory;
            _fightSceneModel = fightSceneModel;
            _playerHUDs = playerHUDs;
            _fightScopesStorage = fightScopesStorage;
        }

        void IStartable.Start()
        {
            _fightSceneModel.OnRoundEnded += RestartFight;

            _fightSceneFactory.CreateFightLocation();
            _fightSceneFactory.CreatePlayers();

            InitializeOpponentsForPlayers();

            CreateFightScopes();
        }

        private void RestartFight()
        {
            DisposeFightScopes();

            _fightSceneStorage.PlayerContainers.Clear();
            _fightSceneStorage.AttackModelsByView.Clear();

            _fightSceneFactory.CreatePlayers();

            InitializeOpponentsForPlayers();

            CreateFightScopes();
        }

        private void InitializeOpponentsForPlayers()
        {
            foreach (var playerContainer in _fightSceneStorage.PlayerContainers)
            {
                var opponentContainer = _fightSceneStorage.PlayerContainers.First(c => c != playerContainer);

                playerContainer.SetOpponent(opponentContainer);
            }
        }

        private void CreateFightScopes()
        {
            _fightScopesFactory.CreateCameraScope();

            foreach (var playerContainer in _fightSceneStorage.PlayerContainers)
            {
                var playerIndex = _fightSceneStorage.PlayerContainers.IndexOf(playerContainer);
                var playersStatsPanel = _playerHUDs[playerIndex];

                _fightScopesFactory.CreatePlayerScope(playerContainer, playersStatsPanel);
            }
        }

        private void DisposeFightScopes()
        {
            _fightScopesStorage.CameraControllerScope.Dispose();

            foreach (var playerLifetimeScope in _fightScopesStorage.PlayerLifetimeScopes)
            {
                playerLifetimeScope.Dispose();
            }

            _fightScopesStorage.PlayerLifetimeScopes.Clear();
        }
    }
}