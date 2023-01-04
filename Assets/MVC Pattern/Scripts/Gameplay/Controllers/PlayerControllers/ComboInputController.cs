using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class ComboInputController : IFixedTickable
    {
        private readonly IStateMachineProxy _stateMachineProxy;

        private readonly ComboState _comboState;

        private readonly PlayerModel _playerModel;

        private readonly ComboModelsContainer _comboModelsContainer;

        private float _comboTimer;

        public ComboInputController(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy,
            ComboState comboState)
        {
            _playerModel = playerContainer.Model;
            _comboModelsContainer = playerContainer.ComboModelsContainer;
            _stateMachineProxy = stateMachineProxy;
            _comboState = comboState;
        }

        void IFixedTickable.FixedTick()
        {
            CountComboTimer();

            if (!_playerModel.IsDoingCombo)
            {
                foreach (var comboModel in _comboModelsContainer.ComboModels)
                {
                    HandleComboInput(comboModel);
                }
            }
        }

        private void HandleComboInput(ComboModel comboModel)
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

                _stateMachineProxy.ChangeState<ComboState>();
            }
        }

        private void CountComboTimer()
        {
            if (_comboTimer >= 0)
            {
                _comboTimer -= Time.fixedDeltaTime;
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