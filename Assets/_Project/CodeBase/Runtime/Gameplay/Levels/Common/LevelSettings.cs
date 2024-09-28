using System;

namespace _Project.CodeBase.Runtime.Gameplay.Levels.Common
{
    [Serializable]
    public struct LevelSettings
    {
        public int KeysNeeded;
        public LevelGenerationMethod GenerationMethod;
    }
}