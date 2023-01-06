using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Views
{
    public abstract class BaseLoadingScreenView : MonoBehaviour
    {
        public abstract UniTask ShowAsync(CancellationToken token);
        public abstract UniTask HideAsync(CancellationToken token);
    }
}