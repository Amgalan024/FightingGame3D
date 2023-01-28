using MVC.Gameplay.Models.Player;
using MVC.Models;
using MVC.StateMachine.States;
using MVC_Pattern.Scripts.Gameplay.Models.StateMachineModels.StateModels;
using MVC_Pattern.Scripts.Gameplay.Services.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerComboController : IFixedTickable
    {
        private readonly IStateMachine _stateMachine;
        private readonly ComboStateModel _comboStateModel;

        private readonly PlayerContainer _playerContainer;

        private float _comboTimer;

        public PlayerComboController(PlayerContainer playerContainer, IStateMachine stateMachine,
            ComboStateModel comboStateModel)
        {
            _playerContainer = playerContainer;
            _stateMachine = stateMachine;
            _comboStateModel = comboStateModel;
        }

        void IFixedTickable.FixedTick()
        {
            // CountComboTimer();
            //
            // if (!_playerContainer.Model.IsDoingCombo)
            // {
            //     foreach (var comboModel in _playerContainer.ComboModelsContainer.ComboModels)
            //     {
            //         HandleComboInput(comboModel);
            //     }
            // }
        }

        private void HandleComboInput(ComboModel comboModel)
        {
            // if (comboModel.InputModels[comboModel.ComboCount].GetInputDown())
            // {
            //     if (comboModel.ComboCount == 0)
            //     {
            //         _comboTimer = 2f;
            //     }
            //
            //     if (_comboTimer > 0)
            //     {
            //         comboModel.ComboCount++;
            //     }
            //     else
            //     {
            //         ResetComboCounts();
            //     }
            // }
            // else if (Input.anyKeyDown)
            // {
            //     comboModel.ComboCount = 0;
            // }
            //
            // if (comboModel.ComboCount == comboModel.InputModels.Length)
            // {
            //     ResetComboCounts();
            //
            //     _comboStateModel.SetData(comboModel.Name, comboModel.Damage);
            //
            //     _stateMachine.ChangeState<ComboState>();
            // }
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
            foreach (var comboModel in _playerContainer.ComboModelsContainer.ComboModels)
            {
                comboModel.ComboCount = 0;
            }
        }
    }
}