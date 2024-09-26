using _Project.CodeBase.Runtime.Factories;
using _Project.CodeBase.Runtime.Services.InputService;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.DI.Global
{
    public class GlobalDependenciesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneService();
            BindAppStateMachine();
            BindGlobalFactories();
            BindInput();
        }

        private void BindSceneService() => Container.Bind(typeof(ISceneLoader), typeof(ISceneEvents)).To<SceneLoader>().AsSingle();

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
    }
}