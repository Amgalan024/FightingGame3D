using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace MVC_Pattern.Scripts.Services.SceneLoader
{
    public interface ISceneLoadService
    {
        UniTask LoadSceneAsync(AssetReference assetReference);
    }
}