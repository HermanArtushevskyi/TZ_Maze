using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character.Common
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
    public class ScriptablePlayerStats: ScriptableObject
    {
        [SerializeField] private PlayerStats _stats;
        
        public PlayerStats GetStats() => _stats;
    }
}