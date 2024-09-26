using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character.Common
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
    public class ScriptablePlayerStats: ScriptableObject
    {
        public PlayerStats Stats;
        
        public PlayerStats GetStats() => Stats;
    }
}