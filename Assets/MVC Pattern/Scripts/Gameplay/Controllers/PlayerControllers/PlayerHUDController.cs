using System;
using MVC.Gameplay.Models.Player;
using UnityEngine.UI;
using VContainer.Unity;

namespace MVC.Gameplay.Controllers
{
    public class PlayerHUDController : IInitializable, IDisposable
    {
        private readonly PlayerHUDView _hudView;
        private readonly PlayerModel _playerModel;

        public PlayerHUDController(PlayerHUDView hudView, PlayerContainer playerContainer)
        {
            _hudView = hudView;
            _playerModel = playerContainer.Model;
        }

        void IInitializable.Initialize()
        {
            _hudView.SetIcon(_playerModel.Icon);

            InitializeSlider(_hudView.HealthBar, _playerModel.MaxHealthPoints, _playerModel.HealthPoints);
            InitializeSlider(_hudView.EnergyBar, _playerModel.MaxEnergyPoints, _playerModel.EnergyPoints);

            _playerModel.OnHPChanged += OnHPChanged;
        }

        void IDisposable.Dispose()
        {
            _playerModel.OnHPChanged -= OnHPChanged;
        }

        private void OnHPChanged(int currentHealthPoints)
        {
            _hudView.HealthBar.value = currentHealthPoints;
        }

        private void InitializeSlider(Slider slider, int maxValue, int startValue)
        {
            slider.minValue = 0;
            slider.maxValue = maxValue;

            slider.value = startValue;
        }
    }
}