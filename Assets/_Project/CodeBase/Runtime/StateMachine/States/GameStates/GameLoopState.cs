using System;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
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
        private readonly IEnemyProvider _enemyProvider;
        private readonly IStateMachine _enemyStateMachine;

        private int _gameTimerId;

        public GameLoopState(ITimer timer, IUpdate update, GameUIActions uiActions,
            ICharacterController characterController, IStateMachine stateMachine,
            IEnemyProvider enemyProvider, IStateMachine enemyStateMachine)
        {
            _timer = timer;
            _update = update;
            _uiActions = uiActions;
            _characterController = characterController;
            _stateMachine = stateMachine;
            _enemyProvider = enemyProvider;
            _enemyStateMachine = enemyStateMachine;
            _gameTimerId = -1;
        }

        public async UniTask Enter()
        {
            _gameTimerId = _timer.StartTimer();
            _uiActions.GetTime = GetTime;
            _update.OnUpdate += Update;
            _characterController.OnDeath += OnDead;
            _uiActions.OnWin += OnWin;
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            if (_gameTimerId != -1)
                _timer.StopTimer(_gameTimerId);
            _enemyProvider.GetEnemy().CanMove = false;
            _enemyStateMachine.Shutdown();
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

        private async void OnWin()
        {
            _uiActions.OnWin -= OnWin;
            await _stateMachine.Enter<GameWonState>();
        }

        private TimeSpan GetTime()
        {
            return _timer.GetTime(_gameTimerId);
        }
    }
}