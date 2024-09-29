using UnityEngine;
using UnityEngine.AI;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces
{
    public interface IEnemy
    {
        public GameObject EnemyOnScene { get; }
        public NavMeshAgent NavMeshAgent { get; }
        public Animator Animator { get; }
        public bool CanMove { get; set; }

        public void Initialize(GameObject enemyObject);
        public void MoveTo(Transform target, bool run = false);
        public string GetDeathMessage();
    }
}