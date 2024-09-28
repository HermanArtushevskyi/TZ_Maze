using System;
using UnityEngine;

namespace _Project.CodeBase.Runtime.UnityContext.Interfaces
{
    public interface ITriggerable
    {
        public event Action<Collider> OnTriggered;
    }
}