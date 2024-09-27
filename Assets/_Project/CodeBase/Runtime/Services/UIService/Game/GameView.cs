using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.Runtime.Services.UIService.Game
{
    public class GameView : MonoBehaviour, IView
    {
        public Canvas Canvas => _canvas;

        public Transform InventoryContent;
        public TextMeshProUGUI TimeText;
        public TextMeshProUGUI KeysText;
        public TextMeshProUGUI HintText;
        public GameObject WinPanel;
        public GameObject LosePanel;
        public GameObject PausePanel;
        public Button ResumeBtn;
        public Button MenuBtn;
        
        [SerializeField] private Canvas _canvas;
    }
}