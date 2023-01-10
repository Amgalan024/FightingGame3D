using Cysharp.Threading.Tasks;
using MVC.Utils.Disposable;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Services
{
    public class LoadingScreenService : DisposableWithCts, ILoadingScreenService
    {
        private readonly LoadingScreensContainer _container;

        public LoadingScreenService(LoadingScreensContainer container)
        {
            _container = container;
        }

        public async UniTask ShowAsync<T>() where T : BaseLoadingScreenView
        {
            await _container.GetLoadingScreenView<T>().ShowAsync(Cts.Token);
        }

        public async UniTask HideAsync<T>() where T : BaseLoadingScreenView
        {
            await _container.GetLoadingScreenView<T>().HideAsync(Cts.Token);
        }
    }
}