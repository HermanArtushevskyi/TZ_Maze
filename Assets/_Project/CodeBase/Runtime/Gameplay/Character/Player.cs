using _Project.CodeBase.Runtime.Gameplay.Character.Common;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using Cinemachine;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    public class Player : IPlayer
    {
        public GameObject SceneObject { get; set; }
        public PlayerStats Stats { get; set; }
        public Camera Camera { get; set; }
        public CinemachineVirtualCamera VirtualCamera { get; set; }
    }
}