using System;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Models;
using MVC.Gameplay.Views.UI;
using MVC.Menu.Models;
using MVC_Pattern.Scripts.Services.SceneLoader;
using MVC_Pattern.Scripts.Services.SceneLoader.Config;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;
using VContainer;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class EndGameController : IInitializable, IDisposable
    {
        private readonly EndGamePanelView _endGamePanelView;
        private readonly FightSceneModel _fightSceneModel;
        private readonly ILoadingScreenService _loadingScreenService;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly EndGameConfig _endGameConfig;
        private readonly SelectedCharactersContainer _selectedCharactersContainer;

        public EndGameController(EndGamePanelView endGamePanelView, FightSceneModel fightSceneModel,
            ISceneLoadService sceneLoadService, ILoadingScreenService loadingScreenService, EndGameConfig endGameConfig,
            SelectedCharactersContainer selectedCharactersContainer)
        {
            _endGamePanelView = endGamePanelView;
            _fightSceneModel = fightSceneModel;
            _sceneLoadService = sceneLoadService;
            _loadingScreenService = loadingScreenService;
            _endGameConfig = endGameConfig;
            _selectedCharactersContainer = selectedCharactersContainer;
        }

        void IInitializable.Initialize()
        {
            _fightSceneModel.OnFightEnded += _endGamePanelView.Show;

            _endGamePanelView.Hide();
            _endGamePanelView.BackToMenu.onClick.AddListener(() => BackToMainMenuAsync().Forget());
            _endGamePanelView.Restart.onClick.AddListener(() => RestartAsync().Forget());
        }

        void IDisposable.Dispose()
        {
            _fightSceneModel.OnFightEnded -= _endGamePanelView.Show;

            _endGamePanelView.BackToMenu.onClick.RemoveAllListeners();
            _endGamePanelView.Restart.onClick.RemoveAllListeners();
        }

        private async UniTask BackToMainMenuAsync()
        {
            await _loadingScreenService.ShowAsync<MainMenuLoadingScreenView>();

            await _sceneLoadService.LoadSceneAsync(_endGameConfig.MainMenuScene);

            await _loadingScreenService.HideAsync<MainMenuLoadingScreenView>();
        }

        private async UniTask RestartAsync()
        {
            using (LifetimeScope.Enqueue(builder => builder.RegisterInstance(_selectedCharactersContainer)))
            {
                await _loadingScreenService.ShowAsync<MainMenuLoadingScreenView>();

                await _sceneLoadService.LoadSceneAsync(_endGameConfig.FightScene);

                await _loadingScreenService.HideAsync<MainMenuLoadingScreenView>();
            }
        }
    }
}