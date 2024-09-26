using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Runtime.Services.UIService.MainMenu
{
    public class MainMenuView : MonoBehaviour, IView
    {
        public Canvas Canvas => _canvas;
        
        [SerializeField] public Button PlayBtn;
        [SerializeField] public Button LeaveBtn;
        [SerializeField] private Canvas _canvas;
    }
}