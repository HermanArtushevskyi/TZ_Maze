using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine.Interfaces
{
    public interface IState : IExitableState
    {
        public UniTask Enter();
    }
}