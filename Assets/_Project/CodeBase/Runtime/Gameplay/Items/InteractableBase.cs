using _Project.CodeBase.Runtime.Gameplay.Items.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Items
{
    [RequireComponent(typeof(Collider))]
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public string HintText => _hintText;
        
        protected const string InteractableLayer = "Interactable";
        [SerializeField] protected string _hintText;

        public abstract void Interact();

        protected virtual void Awake()
        {
            if (gameObject.layer != LayerMask.NameToLayer(InteractableLayer))
                Debug.LogWarning($"Interactable object {gameObject.name} has wrong layer. Should be Interactable");
        }
    }
}