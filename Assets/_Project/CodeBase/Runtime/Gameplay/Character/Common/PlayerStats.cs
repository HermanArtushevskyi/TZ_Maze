using System;

namespace _Project.CodeBase.Runtime.Gameplay.Character.Common
{
    [Serializable]
    public struct PlayerStats
    {
        public int Health;
        public int Damage;
        public float Speed;
        public float RunSpeed;
        public float AttackRange;
        public float AttackRate;
    }
}