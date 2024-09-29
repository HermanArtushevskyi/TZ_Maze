using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    public class EnemyFactory : IFactory<IEnemy>
    {
        private readonly EnemyConfig _enemyConfig;
        private readonly IEnemyProvider _enemyProvider;
        private readonly IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _gameObjectFactory;

        private const string EnemySpawnerTag = "EnemySpawner";
        
        public EnemyFactory(
            EnemyConfig enemyConfig,
            IEnemyProvider enemyProvider,
            IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> gameObjectFactory)
        {
            _enemyConfig = enemyConfig;
            _enemyProvider = enemyProvider;
            _gameObjectFactory = gameObjectFactory;
        }

        public IEnemy Create()
        {
            IEnemy enemy = new ZombieEnemy(_enemyConfig);
            GameObject enemyObject = _gameObjectFactory.Create(_enemyConfig.Prefab,
                GameObject.FindWithTag(EnemySpawnerTag).transform.position, Quaternion.identity, null);
            _enemyProvider.SetEnemy(enemy);
            enemy.Initialize(enemyObject);
            return enemy;
        }
    }
}