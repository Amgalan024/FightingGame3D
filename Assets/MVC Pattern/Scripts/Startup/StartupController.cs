using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace MVC_Pattern.Scripts.Startup
{
    public class StartupController : IInitializable
    {
        private readonly StartupConfig _config;

        public StartupController(StartupConfig config)
        {
            _config = config;
        }

        void IInitializable.Initialize()
        {
            LoadStartScenes().Forget();
        }

        private async UniTask LoadStartScenes()
        {
            var startupScene = SceneManager.GetActiveScene();

            await Addressables.LoadSceneAsync(_config.ServicesScene, LoadSceneMode.Additive);

            await SceneManager.UnloadSceneAsync(startupScene);

            var mainMenuLoadingOperation = Addressables.LoadSceneAsync(_config.MainMenuScene, LoadSceneMode.Additive);

            await mainMenuLoadingOperation;

            var mainMenuScene = mainMenuLoadingOperation.Result.Scene;

            SceneManager.SetActiveScene(mainMenuScene);
        }
    }
}