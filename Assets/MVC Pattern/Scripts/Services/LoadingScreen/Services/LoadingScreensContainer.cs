using System.Collections.Generic;
using System.Linq;
using MVC_Pattern.Scripts.Utils.LoadingScreen.Views;

namespace MVC_Pattern.Scripts.Utils.LoadingScreen.Services
{
    public class LoadingScreensContainer
    {
        private readonly List<BaseLoadingScreenView> _loadingScreenViews;

        public LoadingScreensContainer(List<BaseLoadingScreenView> loadingScreenViews)
        {
            _loadingScreenViews = loadingScreenViews;
        }

        public BaseLoadingScreenView GetLoadingScreenView<T>() where T : BaseLoadingScreenView
        {
            return _loadingScreenViews.First(l => l.GetType() == typeof(T));
        }
    }
}