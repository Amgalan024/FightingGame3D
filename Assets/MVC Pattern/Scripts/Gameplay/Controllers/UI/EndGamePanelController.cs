using System;
using Cysharp.Threading.Tasks;
using MVC.Gameplay.Models;
using MVC.Gameplay.Views.UI;
using MVC_Pattern.Scripts.Services.SceneLoader;
using MVC_Pattern.Scripts.Services.SceneLoader.Config;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class EndGamePanelController : IInitializable, IDisposable
    {
        private readonly EndGamePanelView _endGamePanelView;
        private readonly FightSceneModel _fightSceneModel;
        private readonly ILoadingScreenService _loadingScreenService;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly EndGameConfig _endGameConfig;

        public EndGamePanelController(EndGamePanelView endGamePanelView, FightSceneModel fightSceneModel,
            ISceneLoadService sceneLoadService, ILoadingScreenService loadingScreenService, EndGameConfig endGameConfig)
        {
            _endGamePanelView = endGamePanelView;
            _fightSceneModel = fightSceneModel;
            _sceneLoadService = sceneLoadService;
            _loadingScreenService = loadingScreenService;
            _endGameConfig = endGameConfig;
        }

        void IInitializable.Initialize()
        {
            _endGamePanelView.Hide();
            _endGamePanelView.BackToMenu.onClick.AddListener(() => BackToMainMenuAsync().Forget());
            _fightSceneModel.OnFightEnded += _endGamePanelView.Show;
        }

        void IDisposable.Dispose()
        {
            _endGamePanelView.BackToMenu.onClick.RemoveAllListeners();
            _fightSceneModel.OnFightEnded -= _endGamePanelView.Show;
        }

        private async UniTask BackToMainMenuAsync()
        {
            await _loadingScreenService.ShowAsync<MainMenuLoadingScreenView>();

            await _sceneLoadService.LoadSceneAsync(_endGameConfig.MainMenuScene);

            await _loadingScreenService.HideAsync<MainMenuLoadingScreenView>();
        }
    }
}