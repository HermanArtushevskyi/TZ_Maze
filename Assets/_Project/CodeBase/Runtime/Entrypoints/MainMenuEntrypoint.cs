using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Entrypoints
{
    public class MainMenuEntrypoint : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPrefab;
        
        private IFactory<IView, GameObject> _uiFactory;

        [Inject]
        private void GetDependencies(IFactory<IView, GameObject> uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        private void Start()
        {
            IView view = _uiFactory.Create(_uiPrefab);
        }
    }
}