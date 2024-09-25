using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine.Interfaces
{
    public interface IExitableState
    {
        public UniTask Exit();
    }
}