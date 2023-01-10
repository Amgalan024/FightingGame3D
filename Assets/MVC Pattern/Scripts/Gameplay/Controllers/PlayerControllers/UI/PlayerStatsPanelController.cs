using System;
using MVC.Gameplay.Models.Player;
using UnityEngine.UI;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class PlayerStatsPanelController : IInitializable, IDisposable
    {
        private readonly PlayerStatsPanelView _statsPanelView;
        private readonly PlayerModel _playerModel;

        public PlayerStatsPanelController(PlayerStatsPanelView statsPanelView, PlayerContainer playerContainer)
        {
            _statsPanelView = statsPanelView;
            _playerModel = playerContainer.Model;
        }

        void IInitializable.Initialize()
        {
            _statsPanelView.SetIcon(_playerModel.Icon);

            InitializeSlider(_statsPanelView.HealthBar, _playerModel.MaxHealthPoints, _playerModel.HealthPoints);
            InitializeSlider(_statsPanelView.EnergyBar, _playerModel.MaxEnergyPoints, _playerModel.EnergyPoints);

            _playerModel.OnHPChanged += OnHPChanged;
        }

        void IDisposable.Dispose()
        {
            _playerModel.OnHPChanged -= OnHPChanged;
        }

        private void OnHPChanged(int currentHealthPoints)
        {
            _statsPanelView.HealthBar.value = currentHealthPoints;
        }

        private void InitializeSlider(Slider slider, int maxValue, int startValue)
        {
            slider.minValue = 0;
            slider.maxValue = maxValue;

            slider.value = startValue;
        }
    }
}