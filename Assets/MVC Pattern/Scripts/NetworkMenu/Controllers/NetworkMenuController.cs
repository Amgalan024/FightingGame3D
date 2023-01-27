using System;
using Cysharp.Threading.Tasks;
using MVC.Menu.Services.CharacterSelectionStrategy;
using MVC_Pattern.Scripts.MainMenu.Network;
using MVC_Pattern.Scripts.NetworkMenu.Config;
using MVC_Pattern.Scripts.NetworkMenu.Views;
using MVC_Pattern.Scripts.Services.SceneLoader;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Services;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;
using VContainer;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.NetworkMenu.Controllers
{
    public class NetworkMenuController : IInitializable, IDisposable
    {
        private readonly NetworkMenuConfig _networkMenuConfig;
        private readonly NetworkRoomService _networkRoomService;
        private readonly NetworkMenuView _networkMenuView;
        private readonly NetworkCallbacksHandlersHolder _networkCallbacksHandlersHolder;

        private readonly ISceneLoadService _sceneLoadService;
        private readonly ILoadingScreenService _loadingScreenService;

        public NetworkMenuController(NetworkRoomService networkRoomService, NetworkMenuView networkMenuView,
            NetworkCallbacksHandlersHolder networkCallbacksHandlersHolder, ISceneLoadService sceneLoadService,
            NetworkMenuConfig networkMenuConfig, ILoadingScreenService loadingScreenService)
        {
            _networkRoomService = networkRoomService;
            _networkMenuView = networkMenuView;
            _networkCallbacksHandlersHolder = networkCallbacksHandlersHolder;
            _sceneLoadService = sceneLoadService;
            _networkMenuConfig = networkMenuConfig;
            _loadingScreenService = loadingScreenService;
        }

        void IInitializable.Initialize()
        {
            _networkCallbacksHandlersHolder.MatchmakingCallbacksHandler.OnJoinedRoom += LoadCharacterPvPSelectScene;

            _networkMenuView.CreateRoomButton.onClick.AddListener(() =>
                _networkRoomService.CreateRoom(_networkMenuView.CreateRoomText.text));

            _networkMenuView.JoinRoomButton.onClick.AddListener(() =>
                _networkRoomService.JoinRoom(_networkMenuView.JoinRoomText.text));
        }

        void IDisposable.Dispose()
        {
            _networkCallbacksHandlersHolder.MatchmakingCallbacksHandler.OnJoinedRoom -= LoadCharacterPvPSelectScene;

            _networkMenuView.CreateRoomButton.onClick.RemoveAllListeners();
            _networkMenuView.JoinRoomButton.onClick.RemoveAllListeners();
        }

        private void LoadCharacterPvPSelectScene()
        {
            LoadCharacterPvPSelectSceneAsync().Forget();
        }

        private async UniTaskVoid LoadCharacterPvPSelectSceneAsync()
        {
            using (LifetimeScope.Enqueue(builder =>
                       builder.Register<NetworkPvPCharacterSelectionStrategy>(Lifetime.Singleton)
                           .AsImplementedInterfaces()))
            {
                await _loadingScreenService.ShowAsync<MainMenuLoadingScreenView>();

                await _sceneLoadService.LoadSceneAsync(_networkMenuConfig.CharacterSelectScene);

                await _loadingScreenService.HideAsync<MainMenuLoadingScreenView>();
            }
        }
    }
}