using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Levels
{
    public class PrefabLevelFactory : Factories.Interfaces.IFactory<ILevel>
    {
        private readonly Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _gameObjectFactory;
        private readonly ILevel _level;
        private readonly GameObject _prefab;

        public PrefabLevelFactory(Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> gameObjectFactory,
            ILevel level,
            [InjectOptional(Id = PrefabId.Level)] GameObject prefab)
        {
            _gameObjectFactory = gameObjectFactory;
            _level = level;
            _prefab = prefab;
        }

        public ILevel Create()
        {
            GameObject levelRoot = _gameObjectFactory.Create(_prefab, Vector3.zero, Quaternion.identity, null);
            _level.SetRoot(levelRoot);
            return _level;
        }
    }
}