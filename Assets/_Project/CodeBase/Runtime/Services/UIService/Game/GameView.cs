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
        
        [Space]
        [Header("Texts")]
        public TextMeshProUGUI TimeText;
        public TextMeshProUGUI KeysText;
        public TextMeshProUGUI HintText;
        public TextMeshProUGUI ScrollText;
        public TextMeshProUGUI ResultText;
        public TextMeshProUGUI ResultReasonText;
        
        [Space]
        [Header("Panels")]
        public GameObject MainPanel;
        public GameObject ResultPanel;
        public GameObject PausePanel;
        public GameObject ScrollPanel;
        
        [Space]
        [Header("Buttons")]
        public Button PauseResumeGameBtn;
        public Button PauseToMenuBtn;
        public Button CloseScrollBtn;
        public Button DeathRestartBtn;
        public Button DeathToMenuBtn;
        
        [Space]
        [SerializeField] private Canvas _canvas;
    }
}