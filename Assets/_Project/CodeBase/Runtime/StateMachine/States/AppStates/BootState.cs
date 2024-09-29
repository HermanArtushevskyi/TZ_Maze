using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine.States.AppStates
{
    public class BootState : IState
    {
        private readonly IStateMachine _stateMachine;

        public BootState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            await _stateMachine.Enter<MainLoopState>();
            await UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}