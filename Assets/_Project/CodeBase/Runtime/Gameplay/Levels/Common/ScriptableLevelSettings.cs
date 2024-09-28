using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Levels.Common
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings", order = 1)]
    public class ScriptableLevelSettings : ScriptableObject
    {
        [SerializeField] private LevelSettings _levelSettings;
        
        public LevelSettings GetSettings() => _levelSettings;
    }
}