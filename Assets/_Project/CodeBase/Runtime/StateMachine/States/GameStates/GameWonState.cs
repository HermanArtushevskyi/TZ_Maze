using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine.States.GameStates
{
    public class GameWonState : IState
    {
        private readonly GameUIActions _gameUIActions;

        public GameWonState(GameUIActions gameUIActions)
        {
            _gameUIActions = gameUIActions;
        }

        public async UniTask Enter()
        {
            _gameUIActions.CallWin();
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
        }
    }
}