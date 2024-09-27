using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Items
{
    public class InteractableScroll : InteractableBase
    {
        public override void Interact()
        {
            Debug.Log("Scroll!");
        }
    }
}