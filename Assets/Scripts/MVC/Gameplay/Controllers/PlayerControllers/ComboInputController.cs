using MVC.Configs;
using MVC.Models;
using MVC.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class ComboInputController : IInitializable, ITickable
    {
        private readonly PlayerStateMachine _playerStateMachine;

        private readonly StatesContainer _statesContainer;

        private readonly PlayerModel _playerModel;

        private readonly ComboModelsContainer _comboModelsContainer;

        private float _comboTimer;

        public ComboInputController(PlayerModel playerModel, PlayerStateMachine playerStateMachine,
            StatesContainer statesContainer, ComboModelsContainer comboModelsContainer)
        {
            _statesContainer = statesContainer;
            _comboModelsContainer = comboModelsContainer;
            _playerModel = playerModel;
            _playerStateMachine = playerStateMachine;
        }

        void IInitializable.Initialize()
        {
            throw new System.NotImplementedException();
        }

        void ITickable.Tick()
        {
            CountComboTimer();

            if (!_playerModel.IsDoingCombo)
            {
                foreach (var comboModel in _comboModelsContainer.ComboModels)
                {
                    ComboCheck(comboModel);
                }
            }
        }

        private void ComboCheck(ComboModel comboModel)
        {
            if (Input.GetKeyDown(comboModel.PlayerControlModels[comboModel.ComboCount].Key))
            {
                if (comboModel.ComboCount == 0)
                {
                    _comboTimer = 2f;
                }

                if (_comboTimer > 0)
                {
                    comboModel.ComboCount++;
                }
                else
                {
                    ResetComboCounts();
                }
            }
            else if (Input.anyKeyDown)
            {
                comboModel.ComboCount = 0;
            }

            if (comboModel.ComboCount == comboModel.PlayerControlModels.Length)
            {
                ResetComboCounts();

                _statesContainer.ComboState.Name = comboModel.Name;
                _statesContainer.ComboState.Damage = comboModel.Damage;

                _playerStateMachine.ChangeState(_statesContainer.ComboState);
            }
        }

        private void CountComboTimer()
        {
            if (_comboTimer >= 0)
            {
                _comboTimer -= Time.deltaTime;
            }
        }

        private void ResetComboCounts()
        {
            foreach (var comboModel in _comboModelsContainer.ComboModels)
            {
                comboModel.ComboCount = 0;
            }
        }
    }
}