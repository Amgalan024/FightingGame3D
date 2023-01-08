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

            var servicesSceneLoadOperation = Addressables.LoadSceneAsync(_config.ServicesScene, LoadSceneMode.Additive);

            await servicesSceneLoadOperation;

            await SceneManager.UnloadSceneAsync(startupScene);

            var servicesScene = servicesSceneLoadOperation.Result.Scene;

            var servicesSceneRootObjects = servicesScene.GetRootGameObjects();

            LifetimeScope servicesLifetimeScope = null;

            foreach (var rootObject in servicesSceneRootObjects)
            {
                if (rootObject.TryGetComponent(out servicesLifetimeScope))
                {
                    break;
                }
            }

            using (LifetimeScope.EnqueueParent(servicesLifetimeScope))
            {
                var mainMenuLoadingOperation =
                    Addressables.LoadSceneAsync(_config.MainMenuScene, LoadSceneMode.Additive);

                await mainMenuLoadingOperation;

                var mainMenuScene = mainMenuLoadingOperation.Result.Scene;

                SceneManager.SetActiveScene(mainMenuScene);
            }
        }
    }
}