using _Project.CodeBase.Runtime.StateMachine.Common;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine.Interfaces
{
    public interface IStateMachine
    {
        public IExitableState CurrentState { get; }
        
        public UniTask Initialize();
        public UniTask<TransitionResult> Enter<TState>() where TState : IState;
        public UniTask<TransitionResult> Enter<TState, TPayload>(TPayload payload) where TState : IStateWithPayload<TPayload>;
    }
}