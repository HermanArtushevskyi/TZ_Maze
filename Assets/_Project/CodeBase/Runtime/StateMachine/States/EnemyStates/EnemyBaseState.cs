using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine.States.EnemyStates
{
    public class EnemyBaseState
    {
        protected readonly List<IPlaceOfInterest> PlaceOfInterests;
        
        protected static IAudioAsset Footsteps;

        public EnemyBaseState(List<IPlaceOfInterest> placeOfInterests)
        {
            PlaceOfInterests = placeOfInterests;
        }

        protected Transform GetRandomPlace()
        {
            IPlaceOfInterest place = PlaceOfInterests[Random.Range(0, PlaceOfInterests.Count)];
            return place.Beacon;
        }
    }
}