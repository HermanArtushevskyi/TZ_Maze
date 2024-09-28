using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService.Common;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Cinemachine;
using InputService.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project.CodeBase.Runtime.Services.UIService.Game
{
    public class GamePresenter
    {
        private readonly GameView _gameView;
        private readonly GameUIActions _gameUIActions;
        private readonly IPlayer _player;
        private readonly IUpdate _update;
        private readonly IInputProvider _inputProvider;
        private readonly InputActions _inputActions;
        private readonly IAudioProvider _audioProvider;
        private readonly AudioName _audioNames;
        private readonly ISceneLoader _sceneLoader;
        private readonly SelectiveInputDisabler _inputDisabler;
        private readonly CinemachineInputProvider _cinemachineInputProvider;
        
        private bool _isPaused;
        
        public GamePresenter(IView gameView, GameUIActions gameUIActions, IPlayer player, IUpdate update,
            IInputProvider inputProvider, InputActions inputActions, [InjectOptional(Id = PrefabId.VirtualCamera)] GameObject virtualCameraGO,
            IAudioProvider audioProvider, AudioName audioNames, ISceneLoader sceneLoader)
        {
            _gameView = (GameView) gameView;
            _gameUIActions = gameUIActions;
            _player = player;
            _update = update;
            _inputProvider = inputProvider;
            _inputActions = inputActions;
            _audioProvider = audioProvider;
            _audioNames = audioNames;
            _sceneLoader = sceneLoader;
            _inputDisabler = new SelectiveInputDisabler(new SelectiveInputDisabler.DisabledInput()
            {
                ActionInput = true,
                AttackInput = true,
                MouseDeltaInput = true,
                MovementInput = true,
                RunningInput = true
            });
            _cinemachineInputProvider = virtualCameraGO.GetComponent<CinemachineInputProvider>();
            _isPaused = false;
        }

        public void BindView()
        {
            TurnOnSinglePanel(_gameView.MainPanel);
            _update.OnUpdate += Update;
            
            _gameUIActions.OnOpenScroll += OpenScroll;
            _gameUIActions.OnChangeHint += ChangeHint;
            _gameUIActions.OnDead += OnDead;
            _gameUIActions.OnWin += OnWin;
            _inputActions.UI.ESC.performed += OnPause;
            
            _gameView.DeathRestartBtn.onClick.AddListener(Restart);
            _gameView.DeathToMenuBtn.onClick.AddListener(ToMenu);
            _gameView.PauseResumeGameBtn.onClick.AddListener(UnPause);
            _gameView.PauseToMenuBtn.onClick.AddListener(ToMenu);
            _gameView.CloseScrollBtn.onClick.AddListener(OnCloseScroll);
        }

        private void Update()
        {
            if (_gameUIActions.GetTime != null)
            {
                // Settings timer text in format 00:00
                _gameView.TimeText.text = _gameUIActions.GetTime().ToString("mm':'ss");
            }
        }

        private void OpenScroll(string text)
        {
            _audioProvider.Play(_audioNames.ScrollUnrollSound);
            TurnOnSinglePanel(_gameView.ScrollPanel);
            _gameView.ScrollText.text = text;
            Cursor.lockState = CursorLockMode.None;
            _inputProvider.AddSource(_inputDisabler);
            _cinemachineInputProvider.enabled = false;
        }

        private void OnCloseScroll()
        {
            TurnOnSinglePanel(_gameView.MainPanel);
            Cursor.lockState = CursorLockMode.Locked;
            _inputProvider.RemoveSource(_inputDisabler);
            _cinemachineInputProvider.enabled = true;
        }

        private void OnDead(string deathReason)
        {
            _audioProvider.Play(_audioNames.OnDeathSound);
            TurnOnSinglePanel(_gameView.ResultPanel);
            _gameView.ResultText.text = "Dead";
            _gameView.ResultReasonText.text = deathReason;
            Cursor.lockState = CursorLockMode.None;
            _inputProvider.AddSource(_inputDisabler);
        }

        private void OnWin()
        {
            TurnOnSinglePanel(_gameView.ResultPanel);
            _gameView.ResultText.text = "Congratulations";
            _gameView.ResultReasonText.text = "You found the exit";
            Cursor.lockState = CursorLockMode.None;
            _inputProvider.AddSource(_inputDisabler);
        }

        private void TurnOnSinglePanel(GameObject panel)
        {
            _gameView.MainPanel.SetActive(false);
            _gameView.ResultPanel.SetActive(false);
            _gameView.PausePanel.SetActive(false);
            _gameView.ScrollPanel.SetActive(false);
            
            panel.SetActive(true);
        }

        private void OnPause(InputAction.CallbackContext ctx)
        {
            if (_gameView.ResultPanel.activeSelf)
                return;
            
            if (_gameView.ScrollPanel.activeSelf)
            {
                OnCloseScroll();
                return;
            }
            
            if (_isPaused)
            {
                UnPause();
                return;
            }
            
            _audioProvider.Play(_audioNames.UIClickSound);
            TurnOnSinglePanel(_gameView.PausePanel);
            Cursor.lockState = CursorLockMode.None;
            _inputProvider.AddSource(_inputDisabler);
            _cinemachineInputProvider.enabled = false;
            _isPaused = true;
        }

        private void UnPause()
        {
            _audioProvider.Play(_audioNames.UIClickSound);
            TurnOnSinglePanel(_gameView.MainPanel);
            Cursor.lockState = CursorLockMode.Locked;
            _inputProvider.RemoveSource(_inputDisabler);
            _cinemachineInputProvider.enabled = true;
            _isPaused = false;
        }

        private void ToMenu()
        {
            _audioProvider.Play(_audioNames.UIClickSound);
            _inputProvider.RemoveSource(_inputDisabler);
            _inputActions.UI.ESC.performed -= OnPause;
            _sceneLoader.LoadScene(SceneName.MAIN_MENU);
        }

        private void Restart()
        {
            _audioProvider.Play(_audioNames.UIClickSound);
            _inputProvider.RemoveSource(_inputDisabler);
            _inputActions.UI.ESC.performed -= OnPause;
            _sceneLoader.LoadScene(SceneName.GAME);
        }

        private void ChangeHint(string hint)
        {
            _gameView.HintText.text = hint;
        }
    }
}