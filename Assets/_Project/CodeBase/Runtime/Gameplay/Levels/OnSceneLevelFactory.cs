using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Levels
{
    public class OnSceneLevelFactory : Factories.Interfaces.IFactory<ILevel>
    {
        private readonly ILevel _level;
        private readonly GameObject _levelOnScene;

        public OnSceneLevelFactory(
            ILevel level,
            [InjectOptional(Id = PrefabId.Level)] GameObject levelOnScene)
        {
            _level = level;
            _levelOnScene = levelOnScene;
        }

        public ILevel Create()
        {
            _level.SetRoot(_levelOnScene);
            return _level;
        }
    }
}