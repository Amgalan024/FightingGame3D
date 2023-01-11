using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Startup
{
    public class StartupController : IInitializable
    {
        private readonly StartupConfig _config;
        private readonly StartupLoadingView _startupLoadingView;

        public StartupController(StartupConfig config, StartupLoadingView startupLoadingView)
        {
            _config = config;
            _startupLoadingView = startupLoadingView;
        }

        void IInitializable.Initialize()
        {
            LoadStartScenes().Forget();
        }

        private async UniTask LoadStartScenes()
        {
            //_startupLoadingView.ShowLogo();

            var startupScene = SceneManager.GetActiveScene();

            await Addressables.LoadSceneAsync(_config.ServicesScene, LoadSceneMode.Additive);

            var mainMenuLoadingOperation = Addressables.LoadSceneAsync(_config.MainMenuScene, LoadSceneMode.Additive);

            await mainMenuLoadingOperation;

            var mainMenuScene = mainMenuLoadingOperation.Result.Scene;

            SceneManager.SetActiveScene(mainMenuScene);

            await _startupLoadingView.HideLogoAsync();

            await SceneManager.UnloadSceneAsync(startupScene);
        }
    }
}