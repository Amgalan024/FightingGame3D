using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Views
{
    public class MenuLoadingScreenView : BaseLoadingScreenView
    {
        [SerializeField] private Image _background;
        [SerializeField] private float _duration;

        public override async UniTask ShowAsync(CancellationToken token)
        {
            await ChangeBackgroundColor(token, Color.white, _duration);
        }

        public override async UniTask HideAsync(CancellationToken token)
        {
            await ChangeBackgroundColor(token, Color.clear, _duration);
        }

        private async UniTask ChangeBackgroundColor(CancellationToken token, Color color, float duration)
        {
            var tween = _background.DOColor(color, duration);

            await tween.AwaitForComplete(cancellationToken: token);
        }
    }
}