using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.UIService
{
    public abstract class ViewFactoryBase : IFactory<IView, GameObject>
    {
        private readonly IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> _gameObjectFactory;

        protected ViewFactoryBase(IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
        }

        public virtual IView Create(GameObject viewPrefab)
        {
            GameObject viewGameObject = _gameObjectFactory.Create(viewPrefab, Vector3.zero, Quaternion.identity, null);
            IView view = viewGameObject.GetComponent<IView>();
            return view;
        }
        
        protected IView Create(GameObject viewPrefab, out GameObject viewGameObject)
        {
            viewGameObject = _gameObjectFactory.Create(viewPrefab, Vector3.zero, Quaternion.identity, null);
            IView view = viewGameObject.GetComponent<IView>();
            return view;
        }
    }
}