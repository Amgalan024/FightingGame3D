using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class ComboInputController : IFixedTickable
    {
        private readonly IStateMachineProxy _stateMachineProxy;

        private readonly ComboStateModel _comboStateModel;

        private readonly PlayerModel _playerModel;

        private readonly ComboModelsContainer _comboModelsContainer;

        private float _comboTimer;

        public ComboInputController(PlayerContainer playerContainer, IStateMachineProxy stateMachineProxy,
            ComboStateModel comboStateModel)
        {
            _playerModel = playerContainer.Model;
            _comboModelsContainer = playerContainer.ComboModelsContainer;
            _stateMachineProxy = stateMachineProxy;
            _comboStateModel = comboStateModel;
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

                _comboStateModel.SetData(comboModel.Name, comboModel.Damage);

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