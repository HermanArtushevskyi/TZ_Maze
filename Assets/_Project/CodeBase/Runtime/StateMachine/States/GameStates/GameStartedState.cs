using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine.States.GameStates
{
    public class GameStartedState : IState
    {
        private readonly ITimer _timer;
        private readonly IUpdate _update;

        private int _gameTimerId;

        public GameStartedState(ITimer timer, IUpdate update)
        {
            _timer = timer;
            _update = update;
        }

        public async UniTask Enter()
        {
            _gameTimerId = _timer.StartTimer();
            _update.OnUpdate += Update;
            await UniTask.CompletedTask;
        }

        private void Update()
        {
            Debug.Log(_timer.GetTime(_gameTimerId).Seconds);
        }

        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
        }
    }
}