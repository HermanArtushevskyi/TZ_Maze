namespace _Project.CodeBase.Runtime.Gameplay.Character.Interfaces
{
    public interface ICharacterController
    {
        public IPlayer Player { get; }
        public void SetPlayer(IPlayer player);
        public void Attack();
        public void Action();
        public void Hurt(int damage);
        public void Kill();
    }
}