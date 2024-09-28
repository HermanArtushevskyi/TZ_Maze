using System;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.UnityContext
{
    public class Triggerable : MonoBehaviour, ITriggerable
    {
        public event Action<Collider> OnTriggered;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggered?.Invoke(other);
        }
    }
}