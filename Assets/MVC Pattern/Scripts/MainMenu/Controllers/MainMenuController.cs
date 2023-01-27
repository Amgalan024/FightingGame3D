using System;
using Cysharp.Threading.Tasks;
using MVC.Menu.Configs;
using MVC.Menu.Services.CharacterSelectionStrategy;
using MVC.Utils.Disposable;
using MVC_Pattern.Scripts.MainMenu.Network;
using MVC_Pattern.Scripts.MainMenu.Views;
using MVC_Pattern.Scripts.Services.SceneLoader;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;
using VContainer;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.MainMenu.Controllers
{
    public class MainMenuController : DisposableWithCts, IInitializable
    {
        private readonly MainMenuView _mainMenuView;
        private readonly MainMenuConfig _mainMenuConfig;

        private readonly ILoadingScreenService _screenService;
        private readonly ISceneLoadService _sceneLoadService;

        private readonly NetworkConnectionService _networkConnectionService;
        private readonly NetworkCallbacksHandlersHolder _networkCallbacksHandlersHolder;

        public MainMenuController(MainMenuView mainMenuView, MainMenuConfig mainMenuConfig,
            ILoadingScreenService screenService, ISceneLoadService sceneLoadService,
            NetworkConnectionService networkConnectionService,
            NetworkCallbacksHandlersHolder networkCallbacksHandlersHolder)
        {
            _mainMenuView = mainMenuView;
            _mainMenuConfig = mainMenuConfig;
            _screenService = screenService;
            _sceneLoadService = sceneLoadService;
            _networkConnectionService = networkConnectionService;
            _networkCallbacksHandlersHolder = networkCallbacksHandlersHolder;
        }

        void IInitializable.Initialize()
        {
            _mainMenuView.PlayerVsPlayer.onClick.AddListener(() => LoadPvPCharacterSelectSceneAsync().Forget());

            _mainMenuView.PlayerVsPlayerNetwork.onClick.AddListener(() =>
            {
                _networkConnectionService.Connect();

                _networkCallbacksHandlersHolder.ConnectionCallbacksHandler.OnConnectedToMaster += LoadNetworkScene;
            });
        }

        public override void Dispose()
        {
            base.Dispose();

            _mainMenuView.PlayerVsPlayer.onClick.RemoveAllListeners();
            _mainMenuView.PlayerVsPlayerNetwork.onClick.RemoveAllListeners();

            _networkCallbacksHandlersHolder.ConnectionCallbacksHandler.OnConnectedToMaster -= LoadNetworkScene;
        }

        private void LoadNetworkScene()
        {
            LoadNetworkSceneAsync().Forget();
        }

        private async UniTaskVoid LoadNetworkSceneAsync()
        {
            await _screenService.ShowAsync<MainMenuLoadingScreenView>();

            _sceneLoadService.LoadSceneAsync(_mainMenuConfig.NetworkScene);

            await _screenService.HideAsync<MainMenuLoadingScreenView>();
        }

        private async UniTaskVoid LoadPvPCharacterSelectSceneAsync()
        {
            using (LifetimeScope.Enqueue(builder =>
                       builder.Register<PvPCharacterSelectionStrategy>(Lifetime.Singleton).AsImplementedInterfaces()))
            {
                await _screenService.ShowAsync<MainMenuLoadingScreenView>();

                _sceneLoadService.LoadSceneAsync(_mainMenuConfig.CharacterSelectScene);

                await _screenService.HideAsync<MainMenuLoadingScreenView>();
            }
        }
    }
}