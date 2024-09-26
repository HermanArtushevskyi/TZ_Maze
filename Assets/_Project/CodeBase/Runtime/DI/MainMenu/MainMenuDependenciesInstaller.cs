using _Project.CodeBase.Runtime.Services.UIService;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.DI.MainMenu
{
    public class MainMenuDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            BindCamera();
            BindUIFactory();
        }

        private void BindCamera()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        }

        private void BindUIFactory()
        {
            Container.Bind<Factories.Interfaces.IFactory<IView, GameObject>>().To<MainMenuViewFactory>().AsSingle();
        }
    }
}