using System.Threading;
using Cysharp.Threading.Tasks;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Services
{
    public class LoadingScreenService : ILoadingScreenService
    {
        public async UniTask ShowAsync(BaseLoadingScreenView loadingScreenView, CancellationToken token)
        {
            await loadingScreenView.ShowAsync(token);
        }

        public async UniTask HideAsync(BaseLoadingScreenView loadingScreenView, CancellationToken token)
        {
            await loadingScreenView.HideAsync(token);
        }
    }
}