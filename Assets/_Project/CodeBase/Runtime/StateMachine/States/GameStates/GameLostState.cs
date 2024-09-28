using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine.States.GameStates
{
    public class GameLostState : IStateWithPayload<string>
    {
        private readonly GameUIActions _gameUIActions;

        public GameLostState(GameUIActions gameUIActions)
        {
            _gameUIActions = gameUIActions;
        }

        public async UniTask Enter(string msg)
        {
            _gameUIActions.CallDead(msg);
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
        }
    }
}