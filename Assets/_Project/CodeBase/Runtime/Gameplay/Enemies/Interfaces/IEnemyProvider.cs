namespace _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces
{
    public interface IEnemyProvider
    {
        public IEnemy GetEnemy();
        public void SetEnemy(IEnemy enemy);
    }
}