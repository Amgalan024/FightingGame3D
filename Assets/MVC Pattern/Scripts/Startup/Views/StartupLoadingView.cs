using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MVC_Pattern.Scripts.Startup
{
    public class StartupLoadingView : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _loadingLogo;
        [SerializeField] private Color _blinkColor;
        [SerializeField] private float _stepDuration;

        private Sequence _logoBlinkSequence;

        public void ShowLogo()
        {
            _logoBlinkSequence = DOTween.Sequence();
            _logoBlinkSequence.Append(_loadingLogo.DOColor(_blinkColor, _stepDuration));
            _logoBlinkSequence.Append(_loadingLogo.DOColor(Color.white, _stepDuration));
            _logoBlinkSequence.SetLoops(-1);
        }

        public async UniTask HideLogoAsync()
        {
            if (_logoBlinkSequence.IsActive())
            {
                _logoBlinkSequence.Kill();
            }

            var sequence = DOTween.Sequence();
            sequence.Join(_background.DOColor(Color.clear, _stepDuration));
            sequence.Join(_loadingLogo.DOColor(Color.clear, _stepDuration));

            await sequence.AwaitForComplete();
        }
    }
}