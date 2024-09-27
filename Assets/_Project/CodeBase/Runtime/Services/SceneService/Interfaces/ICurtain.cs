using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.Services.SceneService.Interfaces
{
    public interface ICurtain
    {
        public UniTask Open();
        public UniTask Close();
    }
}