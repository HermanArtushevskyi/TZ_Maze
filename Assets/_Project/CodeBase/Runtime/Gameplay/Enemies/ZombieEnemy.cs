using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    public class ZombieEnemy : EnemyBase
    {
        public ZombieEnemy(EnemyConfig enemyConfig) : base(enemyConfig) { }
        
        public override string GetDeathMessage() => "Zombie has eaten your brain";
    }
}