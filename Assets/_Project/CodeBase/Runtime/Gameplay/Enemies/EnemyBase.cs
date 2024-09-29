using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    public abstract class EnemyBase : IEnemy
    {
        public GameObject EnemyOnScene { get; protected set; }
        public NavMeshAgent NavMeshAgent { get; protected set; }
        public Animator Animator { get; protected set; }
        public bool CanMove { get; set; }

        private readonly EnemyConfig _enemyConfig;

        private static readonly int SpeedParameterHash = Animator.StringToHash("Speed");

        protected EnemyBase(EnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
        }

        public void MoveTo(Transform target, bool run = false)
        {
            NavMeshAgent.SetDestination(target.position);
            NavMeshAgent.speed = run ? _enemyConfig.ChasingSpeed : _enemyConfig.WanderingSpeed;
            float animatorSpeed = run ? 1f : 0.2f;
            Animator.SetFloat(SpeedParameterHash, animatorSpeed);
        }

        public void Initialize(GameObject enemyObject)
        {
            EnemyOnScene = enemyObject;
            NavMeshAgent = enemyObject.GetComponent<NavMeshAgent>();
            Animator = enemyObject.GetComponentInChildren<Animator>();
            CanMove = true;
        }

        public abstract string GetDeathMessage();
    }
}