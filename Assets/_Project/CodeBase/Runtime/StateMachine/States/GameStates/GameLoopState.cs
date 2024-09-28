using System;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine.States.GameStates
{
    public class GameLoopState : IState
    {
        private readonly ITimer _timer;
        private readonly IUpdate _update;
        private readonly GameUIActions _uiActions;
        private readonly ICharacterController _characterController;
        private readonly IStateMachine _stateMachine;

        private int _gameTimerId;

        public GameLoopState(ITimer timer, IUpdate update, GameUIActions uiActions,
            ICharacterController characterController, IStateMachine stateMachine)
        {
            _timer = timer;
            _update = update;
            _uiActions = uiActions;
            _characterController = characterController;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            _gameTimerId = _timer.StartTimer();
            _uiActions.GetTime = GetTime;
            _update.OnUpdate += Update;
            _characterController.OnDeath += OnDead;
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            _timer.StopTimer(_gameTimerId);
            _update.OnUpdate -= Update;
            await UniTask.CompletedTask;
        }

        /// <summary>
        /// Called every frame while game is active
        /// </summary>
        private void Update()
        {
            
        }

        private async void OnDead(string deathMsg)
        {
            await _stateMachine.Enter<GameLostState, string>(deathMsg);
        }

        private TimeSpan GetTime()
        {
            return _timer.GetTime(_gameTimerId);
        }
    }
}