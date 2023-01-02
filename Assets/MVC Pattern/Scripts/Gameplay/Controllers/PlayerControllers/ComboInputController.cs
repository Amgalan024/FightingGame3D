using MVC.Models;
using MVC.StateMachine.States;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class ComboInputController : ITickable
    {
        private readonly StateMachine.StateMachine _stateMachine;

        private readonly ComboState _comboState;

        private readonly PlayerModel _playerModel;

        private readonly ComboModelsContainer _comboModelsContainer;

        private float _comboTimer;

        public ComboInputController(PlayerModel playerModel, StateMachine.StateMachine stateMachine,
            ComboModelsContainer comboModelsContainer, ComboState comboState)
        {
            _comboModelsContainer = comboModelsContainer;
            _comboState = comboState;
            _playerModel = playerModel;
            _stateMachine = stateMachine;
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
            if (Input.GetKeyDown(comboModel.InputModels[comboModel.ComboCount].Key))
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

            if (comboModel.ComboCount == comboModel.InputModels.Length)
            {
                ResetComboCounts();

                _comboState.Name = comboModel.Name;
                _comboState.Damage = comboModel.Damage;

                _stateMachine.ChangeState(_comboState);
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