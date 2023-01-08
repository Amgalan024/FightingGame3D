using System.Threading;
using Cysharp.Threading.Tasks;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Services
{
    public interface ILoadingScreenService
    {
        UniTask ShowAsync<T>(CancellationToken token) where T : BaseLoadingScreenView;
        UniTask HideAsync<T>(CancellationToken token) where T : BaseLoadingScreenView;
    }
}