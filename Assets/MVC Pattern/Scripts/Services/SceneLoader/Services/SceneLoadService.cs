using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace MVC_Pattern.Scripts.Services.SceneLoader
{
    public class SceneLoadService : ISceneLoadService
    {
        public async UniTask LoadSceneAsync(AssetReference assetReference)
        {
            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            var sceneLoadOperation = Addressables.LoadSceneAsync(assetReference, LoadSceneMode.Additive);

            await sceneLoadOperation;

            SceneManager.SetActiveScene(sceneLoadOperation.Result.Scene);
        }
    }
}