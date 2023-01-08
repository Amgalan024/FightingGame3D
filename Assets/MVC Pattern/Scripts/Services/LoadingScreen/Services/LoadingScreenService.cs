using System.Threading;
using Cysharp.Threading.Tasks;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Services
{
    public class LoadingScreenService : ILoadingScreenService
    {
        private readonly LoadingScreensContainer _container;

        public LoadingScreenService(LoadingScreensContainer container)
        {
            _container = container;
        }

        public async UniTask ShowAsync<T>(CancellationToken token) where T : BaseLoadingScreenView
        {
            await _container.GetLoadingScreenView<T>().ShowAsync(token);
        }

        public async UniTask HideAsync<T>(CancellationToken token) where T : BaseLoadingScreenView
        {
            await _container.GetLoadingScreenView<T>().HideAsync(token);
        }
    }
}