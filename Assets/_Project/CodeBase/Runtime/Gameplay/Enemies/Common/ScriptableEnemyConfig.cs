using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies.Common
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "EnemyConfig", order = 0)]
    public class ScriptableEnemyConfig : ScriptableObject
    {
        [SerializeField] private EnemyConfig _config;
        
        public EnemyConfig GetConfig() => _config;
    }
}