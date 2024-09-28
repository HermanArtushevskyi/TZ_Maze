namespace _Project.CodeBase.Runtime.Gameplay.Traps.Interfaces
{
    public interface ITrap
    {
        public string GetDeathMessage();
        public void Trigger();
    }
}