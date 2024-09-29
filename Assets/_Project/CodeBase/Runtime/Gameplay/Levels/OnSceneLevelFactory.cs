using System.Collections.Generic;
using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Levels
{
    public class OnSceneLevelFactory : Factories.Interfaces.IFactory<ILevel>
    {
        private readonly ILevel _level;
        private readonly List<IPlaceOfInterest> _placeOfInterests;
        private readonly GameObject _levelOnScene;

        public OnSceneLevelFactory(
            ILevel level,
            List<IPlaceOfInterest> placeOfInterests,
            [InjectOptional(Id = PrefabId.Level)] GameObject levelOnScene)
        {
            _level = level;
            _placeOfInterests = placeOfInterests;
            _levelOnScene = levelOnScene;
        }

        public ILevel Create()
        {
            _level.SetRoot(_levelOnScene);
            var places = _levelOnScene.GetComponentsInChildren<IPlaceOfInterest>();
            _placeOfInterests.AddRange(places);
            return _level;
        }
    }
}