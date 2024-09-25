using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.States.AppStates;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine
{
    public class AppStateMachine : StateMachineBase
    {
        public AppStateMachine(ISceneLoader sceneLoader) : base()
        {
            States = new();
            States.Add(typeof(BootState), new BootState(this));
            States.Add(typeof(MainLoopState), new MainLoopState(sceneLoader));
        }

        public override UniTask Initialize()
        {
            Enter<BootState>();
            return UniTask.CompletedTask;
        }
    }
}