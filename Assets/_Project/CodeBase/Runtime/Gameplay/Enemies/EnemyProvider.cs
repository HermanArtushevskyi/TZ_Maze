using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    public class EnemyProvider : IEnemyProvider
    {
        private IEnemy _enemy;
        
        public IEnemy GetEnemy()
        {
            return _enemy;
        }

        public void SetEnemy(IEnemy enemy)
        {
            _enemy = enemy;
        }
    }
}