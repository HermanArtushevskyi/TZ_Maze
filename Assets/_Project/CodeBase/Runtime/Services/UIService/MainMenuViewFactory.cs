using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.MainMenu;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Services.UIService
{
    public class MainMenuViewFactory : ViewFactoryBase
    {
        private readonly DiContainer _container;
        private readonly Camera _camera;

        public MainMenuViewFactory(
            Factories.Interfaces.IFactory<GameObject, GameObject, Vector3, Quaternion, Transform> gameObjectFactory,
            DiContainer container,
            Camera camera) : base(gameObjectFactory)
        {
            _container = container;
            _camera = camera;
        }

        public override IView Create(GameObject viewPrefab)
        {
            IView view = base.Create(viewPrefab, out _);
            view.Canvas.worldCamera = _camera;
            _container.Bind<IView>().FromInstance(view).AsSingle();
            _container.Instantiate<MainMenuPresenter>().BindView();
            return view;
        }
    }
}