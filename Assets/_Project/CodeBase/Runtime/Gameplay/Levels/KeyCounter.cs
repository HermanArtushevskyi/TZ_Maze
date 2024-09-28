using System;
using _Project.CodeBase.Runtime.Gameplay.Levels.Common;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;

namespace _Project.CodeBase.Runtime.Gameplay.Levels
{
    public class KeyCounter : IKeyCounter
    {
        public event Action OnAllKeyGathered;
        public event Action OnKeyGathered;
        
        private int _gatheredKeys;
        private int _neededKeys;
        
        public KeyCounter(LevelSettings levelSettings)
        {
            _neededKeys = levelSettings.KeysNeeded;
            _gatheredKeys = 0;
        }
        
        public void AddKey()
        {
            _gatheredKeys++;
            OnKeyGathered?.Invoke();
            if (_gatheredKeys == _neededKeys)
                OnAllKeyGathered?.Invoke();
        }
        
        public int GetAmountOfGatheredKeys() => _gatheredKeys;
        
        public int GetAmountOfNeededKeys() => _neededKeys;
    }
}