using _Project.CodeBase.Runtime.Factories;
using _Project.CodeBase.Runtime.Gameplay.Character.Common;
using _Project.CodeBase.Runtime.Services.AudioService;
using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.Services.TimerService;
using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using _Project.CodeBase.Runtime.UnityContext;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.DI.Global
{
    public class GlobalDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private ScriptablePlayerStats _playerStats;
        [SerializeField] private MonoContext _monoContext;
        [SerializeField] private AudioName _audioName;
        [SerializeField] private GameObject _curtainPrefab;
        
        public override void InstallBindings()
        {
            BindMonoContext();
            BindTimerService();
            BindSceneService();
            BindAppStateMachine();
            BindGlobalFactories();
            BindInput();
            BindAudio();
            BindConfigs();
        }

        private void BindTimerService()
        {
            Container.Bind<ITimer>().To<Timer>().AsSingle();
        }

        private void BindMonoContext() => Container.Bind(typeof(IUpdate), typeof(IFixedUpdate)).FromInstance(_monoContext).AsSingle();

        private void BindSceneService()
        {
            GameObject curtain = Container.InstantiatePrefab(_curtainPrefab);
            Container.Bind<ICurtain>().FromInstance(curtain.GetComponent<ICurtain>()).AsSingle();
            Container.Bind(typeof(ISceneLoader), typeof(ISceneEvents)).To<SceneLoader>().AsSingle();
        }

        private void BindAppStateMachine() => Container.Bind<IStateMachine>().WithId(StateMachineId.App).To<AppStateMachine>().AsCached();

        private void BindGlobalFactories()
        {
            Container.Bind<Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform>>().To<GameObjectFactory>().AsSingle();
        }

        private void BindInput()
        {
            IInputProvider inputProvider = new InputProvider();
            inputProvider.AddSource(new InputClearer());
            inputProvider.AddSource(new UnityInputSource());
            Container.Bind<IInputProvider>().FromInstance(inputProvider).AsSingle();
        }

        private void BindAudio()
        {
            Container.Bind<IAudioProvider>().To<FMODProvider>().AsSingle();
            Container.Bind<AudioName>().FromInstance(_audioName).AsSingle();
        }

        private void BindConfigs()
        {
            #if UNITY_EDITOR
            if (_playerStats == null) Debug.LogError($"PlayerStats config is not set in {nameof(GlobalDependenciesInstaller)}");
            #endif
            Container.Bind<PlayerStats>().FromInstance(_playerStats.GetStats()).AsSingle();
        }
    }
}