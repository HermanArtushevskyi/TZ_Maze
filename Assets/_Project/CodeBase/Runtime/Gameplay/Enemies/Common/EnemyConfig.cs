using System;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies.Common
{
    [Serializable]
    public struct EnemyConfig
    {
        public float FOV;
        public float ViewDistance;
        public float ChaseDistance;
        public float WanderingSpeed;
        public float ChasingSpeed;
        public GameObject Prefab;
    }
}