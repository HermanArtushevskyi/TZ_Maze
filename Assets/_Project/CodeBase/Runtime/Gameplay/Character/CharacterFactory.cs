using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character.Common;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using Cinemachine;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    public class CharacterFactory : IFactory<GameObject, GameObject, Transform>
    {
        private readonly PlayerStats _stats;
        private readonly IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _gameObjectFactory;
        private readonly IPlayer _player;
        private readonly ICharacterController _characterController;

        private const string CinemachineTag = "CinemachineVC";
        private const string HeadTag = "Head";
        
        public CharacterFactory(
            PlayerStats stats,
            IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> gameObjectFactory,
            IPlayer player,
            ICharacterController characterController)
        {
            _stats = stats;
            _gameObjectFactory = gameObjectFactory;
            _player = player;
            _characterController = characterController;
        }

        public GameObject Create(GameObject playerPrefab, Transform spawnPoint)
        {
            GameObject character = _gameObjectFactory.Create(playerPrefab, spawnPoint.position, spawnPoint.rotation, null);
            _player.SceneObject = character;
            _player.Stats = _stats;
            _player.Camera = Camera.main;
            _player.VirtualCamera = GameObject.FindWithTag(CinemachineTag).GetComponent<CinemachineVirtualCamera>();
            _player.VirtualCamera.Follow = GameObject.FindWithTag(HeadTag).transform;
            _characterController.SetPlayer(_player);
            return character;
        }
    }
}