using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine.States.GameStates
{
    public class JustLoadedState : IState
    {
        private readonly ICurtain _curtain;
        private readonly IFactory<GameObject, GameObject, Transform> _playerFactory;
        private readonly IFactory<IView, GameObject> _viewFactory;
        private readonly GameObject _uiPrefab;
        private readonly GameObject _playerPrefab;
        private readonly IStateMachine _stateMachine;

        private const string SpawnpointTag = "Spawnpoint";
        
        public JustLoadedState(
            ICurtain curtain,
            IFactory<GameObject, GameObject, Transform> playerFactory,
            IFactory<IView, GameObject> viewFactory,
            GameObject uiPrefab,
            GameObject playerPrefab,
            IStateMachine stateMachine)
        {
            _curtain = curtain;
            _playerFactory = playerFactory;
            _viewFactory = viewFactory;
            _uiPrefab = uiPrefab;
            _playerPrefab = playerPrefab;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _playerFactory.Create(_playerPrefab, GameObject.FindWithTag(SpawnpointTag).transform);
            _viewFactory.Create(_uiPrefab);
            await _stateMachine.Enter<GameStartedState>();
        }

        public async UniTask Exit()
        {
            await _curtain.Open();
        }
    }
}