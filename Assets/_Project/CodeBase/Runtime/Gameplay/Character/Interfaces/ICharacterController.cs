using System;

namespace _Project.CodeBase.Runtime.Gameplay.Character.Interfaces
{
    public interface ICharacterController
    {
        public IPlayer Player { get; }
        public void SetPlayer(IPlayer player);
        public event Action<string> OnDeath;
    }
}