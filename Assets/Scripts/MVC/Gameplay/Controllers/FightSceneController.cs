using MVC.Gameplay.Models;
using MVC.Gameplay.Services;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class FightSceneController : IInitializable
    {
        private readonly FightSceneFactory _factory;
        private readonly FightSceneStorage _storage;
        private readonly PlayerLifetimeScopeFactory _playerLifetimeScopeFactory;

        private readonly FightSceneModel _fightSceneModel;

        public FightSceneController(FightSceneFactory factory, FightSceneStorage storage,
            PlayerLifetimeScopeFactory playerLifetimeScopeFactory, FightSceneModel fightSceneModel)
        {
            _factory = factory;
            _storage = storage;
            _playerLifetimeScopeFactory = playerLifetimeScopeFactory;
            _fightSceneModel = fightSceneModel;
        }

        public void Initialize()
        {
            _factory.CreateFightLocation();
            _factory.CreatePlayers();

            foreach (var modelView in _storage.PlayerModelsByView)
            {
                var playerLifetimeScope =
                    _playerLifetimeScopeFactory.CreatePlayerLifetimeScope(modelView.Value, modelView.Key);

                _fightSceneModel.PlayerLifetimeScopes.Add(playerLifetimeScope);
            }

            _storage.PlayerModels[0].OnLose += _storage.PlayerModels[1].ScoreWin;
            _storage.PlayerModels[1].OnLose += _storage.PlayerModels[0].ScoreWin;

        }
    }
}