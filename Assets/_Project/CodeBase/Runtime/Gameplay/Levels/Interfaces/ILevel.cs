using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces
{
    public interface ILevel
    {
        public GameObject Root { get; }
        public void SetRoot(GameObject levelRoot);
    }
}