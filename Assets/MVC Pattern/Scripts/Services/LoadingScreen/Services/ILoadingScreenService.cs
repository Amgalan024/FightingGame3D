using System.Threading;
using Cysharp.Threading.Tasks;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Services
{
    public interface ILoadingScreenService
    {
        UniTask ShowAsync<T>() where T : BaseLoadingScreenView;
        UniTask HideAsync<T>() where T : BaseLoadingScreenView;
    }
}