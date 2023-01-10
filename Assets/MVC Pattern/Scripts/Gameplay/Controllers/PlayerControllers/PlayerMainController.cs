using MVC.Gameplay.Models.Player;
using MVC.StateMachine.States;
using VContainer.Unity;

namespace MVC.Controllers
{
    public class PlayerMainController : IInitializable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerContainer _opponentContainer;
        private readonly StateMachine.StateMachine _stateMachine;

        public PlayerMainController(PlayerContainer playerContainer, StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _playerModel = playerContainer.Model;
            _opponentContainer = playerContainer.OpponentContainer;
        }

        public void Initialize()
        {
            _playerModel.OnLose += HandleLose;
            _playerModel.OnWin += HandleWin;
        }

        private void HandleLose()
        {
            _opponentContainer.Model.ScoreWin();
            _stateMachine.ChangeState<LoseState>();
        }

        private void HandleWin()
        {
            _stateMachine.ChangeState<WinState>();
        }
    }
}