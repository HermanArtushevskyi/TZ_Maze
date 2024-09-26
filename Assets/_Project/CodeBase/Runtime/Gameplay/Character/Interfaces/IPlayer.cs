using _Project.CodeBase.Runtime.Gameplay.Character.Common;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character.Interfaces
{
    public interface IPlayer
    {
        public GameObject SceneObject { get; set; }
        public PlayerStats Stats { get; set; }
    }
}