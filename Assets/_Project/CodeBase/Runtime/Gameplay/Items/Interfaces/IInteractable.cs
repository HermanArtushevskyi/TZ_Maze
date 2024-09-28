namespace _Project.CodeBase.Runtime.Gameplay.Items.Interfaces
{
    /// <summary>
    /// Implement this interface to make gameobject interactable with character controller
    /// </summary>
    public interface IInteractable
    {
        public string HintText { get; }
        public void Interact();
    }
}