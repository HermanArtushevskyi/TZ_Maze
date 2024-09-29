using System;
using _Project.CodeBase.Runtime.Common;

namespace _Project.CodeBase.Runtime.Gameplay.Character.Interfaces
{
    public interface ICharacterController
    {
        public IPlayer Player { get; }
        public AABB PlayerAABB { get; }
        public void SetPlayer(IPlayer player);
        public event Action<string> OnDeath;
    }
}