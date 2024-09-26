using _Project.CodeBase.Runtime.Factories.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Factories
{
    /// <summary>
    /// A wrapper around Object.Instantiate
    /// </summary>
    public class GameObjectFactory : IFactory<GameObject, GameObject, Vector3, Quaternion, Transform>
    {
        public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject gameObject = Object.Instantiate(prefab, position, rotation, parent);
            return gameObject;
        }
    }
}