using System;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.UnityContext
{
    public class MonoContext : MonoBehaviour, IUpdate, IFixedUpdate
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        
        public void Update()
        {
            OnUpdate?.Invoke();
        }
        
        public void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }
}