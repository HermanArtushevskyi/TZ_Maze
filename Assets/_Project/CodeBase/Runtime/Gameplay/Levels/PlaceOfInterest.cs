using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Levels
{
    public class PlaceOfInterest : MonoBehaviour, IPlaceOfInterest
    {
        public Transform Beacon => transform;
    }
}