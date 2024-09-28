using _Project.CodeBase.Runtime.Gameplay.Traps.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Traps
{
    public abstract class TrapBase : MonoBehaviour, ITrap
    {
        public abstract string GetDeathMessage();
        public abstract void Trigger();
    }
}