using _Project.CodeBase.Runtime.Services.UIService.Game;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Services.UIService
{
    public class GameViewFactory : ViewFactoryBase
    {
        private readonly DiContainer _container;

        public GameViewFactory(
            Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> gameObjectFactory,
            DiContainer container) : base(gameObjectFactory)
        {
            _container = container;
        }
        
        public override IView Create(GameObject viewPrefab)
        {
            IView view = base.Create(viewPrefab, out _);
            view.Canvas.worldCamera = Camera.allCameras[1];
            _container.Bind<IView>().FromInstance(view).AsSingle();
            _container.Instantiate<GamePresenter>().BindView();
            return view;
        }
    }
}