using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Gameplay.Character;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService;
using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using UnityEngine;
using Zenject;
using CharacterController = _Project.CodeBase.Runtime.Gameplay.Character.CharacterController;
using IFactories = _Project.CodeBase.Runtime.Factories.Interfaces;

namespace _Project.CodeBase.Runtime.DI.Game
{
    public class GameDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _gameViewPrefab;
        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindUI();
            BindCamera();
            BindStateMachine();
        }

        private void BindPlayer()
        {
            Container.Bind<GameObject>().WithId(PrefabId.Player).FromInstance(_playerPrefab).AsCached();
            Container.Bind<IPlayer>().To<Player>().AsSingle();
            Container.Bind<ICharacterController>().To<CharacterController>().AsSingle();
            Container.Bind<IFactories.IFactory<GameObject, GameObject, Transform>>().To<CharacterFactory>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<GameObject>().WithId(PrefabId.UI).FromInstance(_gameViewPrefab).AsCached();
            Container.Bind<IFactories.IFactory<IView, GameObject>>().To<GameViewFactory>().AsSingle();
            Container.Bind<GameUIActions>().To<GameUIActions>().AsSingle();
        }

        private void BindCamera()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateMachine>().WithId(StateMachineId.Game).To<GameStateMachine>().AsCached();
        }
    }
}