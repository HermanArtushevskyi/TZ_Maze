using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;
using InputService.Common;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine.States.GameStates
{
    public class JustLoadedState : IState
    {
        private readonly ICurtain _curtain;
        private readonly IFactory<GameObject, GameObject, Transform> _playerFactory;
        private readonly IFactory<IView, GameObject> _viewFactory;
        private readonly IFactory<ILevel> _levelFactory;
        private readonly IFactory<IEnemy> _enemyFactory;
        private readonly IStateMachine _enemyStateMachine;
        private readonly IInputProvider _inputProvider;
        private readonly InputActions _inputActions;
        private readonly GameObject _uiPrefab;
        private readonly GameObject _playerPrefab;
        private readonly IStateMachine _stateMachine;

        private const string SpawnpointTag = "Spawnpoint";
        
        public JustLoadedState(ICurtain curtain,
            IFactory<GameObject, GameObject, Transform> playerFactory,
            IFactory<IView, GameObject> viewFactory,
            IFactory<ILevel> levelFactory,
            IFactory<IEnemy> enemyFactory,
            IStateMachine enemyStateMachine,
            IInputProvider inputProvider,
            InputActions inputActions,
            GameObject uiPrefab,
            GameObject playerPrefab,
            IStateMachine stateMachine)
        {
            _curtain = curtain;
            _playerFactory = playerFactory;
            _viewFactory = viewFactory;
            _levelFactory = levelFactory;
            _enemyFactory = enemyFactory;
            _enemyStateMachine = enemyStateMachine;
            _inputProvider = inputProvider;
            _inputActions = inputActions;
            _uiPrefab = uiPrefab;
            _playerPrefab = playerPrefab;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            _levelFactory.Create();
            Cursor.lockState = CursorLockMode.Locked;
            _playerFactory.Create(_playerPrefab, GameObject.FindWithTag(SpawnpointTag).transform);
            _viewFactory.Create(_uiPrefab);
            _enemyFactory.Create();
            _inputProvider.Sources.Clear();
            _inputProvider.AddSource(new InputClearer());
            _inputProvider.AddSource(new UnityInputSource(_inputActions));
            await _enemyStateMachine.Initialize();
            await _stateMachine.Enter<GameLoopState>();
        }

        public async UniTask Exit()
        {
            await _curtain.Open();
        }
    }
}