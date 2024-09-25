using _Project.CodeBase.Runtime.Services.SceneService;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Zenject;

namespace _Project.CodeBase.Runtime.DI.Global
{
    public class GlobalDependenciesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneService();
            BindAppStateMachine();
        }

        private void BindSceneService() => Container.Bind(typeof(ISceneLoader), typeof(ISceneEvents)).To<SceneLoader>().AsSingle();

        private void BindAppStateMachine() => Container.Bind<IStateMachine>().WithId(StateMachineId.App).To<AppStateMachine>().AsCached();
    }
}