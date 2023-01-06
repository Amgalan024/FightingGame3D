using System.Threading;
using Cysharp.Threading.Tasks;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Services
{
    public interface ILoadingScreenService
    {
        UniTask ShowAsync(BaseLoadingScreenView loadingScreenView, CancellationToken token);
        UniTask HideAsync(BaseLoadingScreenView loadingScreenType, CancellationToken token);
    }
}