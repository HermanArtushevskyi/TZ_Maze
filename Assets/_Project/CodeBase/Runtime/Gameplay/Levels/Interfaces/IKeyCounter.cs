using System;

namespace _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces
{
    public interface IKeyCounter
    {
        public event Action OnAllKeyGathered;
        public event Action OnKeyGathered;
        
        public void AddKey();
        public int GetAmountOfGatheredKeys();
        public int GetAmountOfNeededKeys();
    }
}