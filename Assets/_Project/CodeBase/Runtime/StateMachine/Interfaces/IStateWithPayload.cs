using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine.Interfaces
{
    public interface IStateWithPayload<T> : IExitableState
    {
        public UniTask Enter(T payload);
    }
}