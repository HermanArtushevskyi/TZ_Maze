using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Levels
{
    public class Level : ILevel
    {
        public GameObject Root { get; private set; }

        public void SetRoot(GameObject levelRoot)
        {
            Root = levelRoot;
        }
    }
}