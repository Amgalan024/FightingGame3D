using Cysharp.Threading.Tasks;
using MVC.Menu.Configs;
using MVC.Utils.Disposable;
using MVC_Pattern.Scripts.MainMenu.Views;
using MVC_Pattern.Scripts.Services.SceneLoader;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.MainMenu.Controllers
{
    public class MainMenuController : DisposableWithCts, IInitializable
    {
        private readonly MainMenuView _mainMenuView;
        private readonly MainMenuConfig _mainMenuConfig;

        private readonly ILoadingScreenService _screenService;
        private readonly ISceneLoadService _sceneLoadService;

        public MainMenuController(MainMenuView mainMenuView, MainMenuConfig mainMenuConfig,
            ILoadingScreenService screenService, ISceneLoadService sceneLoadService)
        {
            _mainMenuView = mainMenuView;
            _mainMenuConfig = mainMenuConfig;
            _screenService = screenService;
            _sceneLoadService = sceneLoadService;
        }

        void IInitializable.Initialize()
        {
            _mainMenuView.PlayerVsPlayer.onClick.AddListener(() => LoadPvPCharacterSelectScene().Forget());
        }

        public override void Dispose()
        {
            base.Dispose();

            _mainMenuView.PlayerVsPlayer.onClick.RemoveAllListeners();
        }

        private async UniTaskVoid LoadPvPCharacterSelectScene()
        {
            await _screenService.ShowAsync<MainMenuLoadingScreenView>();

            _sceneLoadService.LoadSceneAsync(_mainMenuConfig.CharacterSelectScene);

            await _screenService.HideAsync<MainMenuLoadingScreenView>();
        }
    }
}