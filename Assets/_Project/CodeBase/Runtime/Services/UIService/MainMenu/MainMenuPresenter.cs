using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService.Common;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.UIService.MainMenu
{
    public class MainMenuPresenter
    {
        private readonly MainMenuView MainMenuView;
        private readonly IAudioProvider _audioProvider;
        private readonly AudioName _audioName;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _curtain;

        public MainMenuPresenter(IView mainMenuView, IAudioProvider audioProvider, AudioName audioName,
            ISceneLoader sceneLoader, ICurtain curtain)
        {
            MainMenuView = (MainMenuView) mainMenuView;
            _audioProvider = audioProvider;
            _audioName = audioName;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void BindView()
        {
            MainMenuView.LeaveBtn.onClick.AddListener(OnLeave);
            MainMenuView.PlayBtn.onClick.AddListener(OnPlay);
        }

        private async void OnPlay()
        {
            _audioProvider.Play(_audioName.UIClickSound);
            UniTask curtainTask = _curtain.Close();
            UniTask.WhenAll(curtainTask);
            await _sceneLoader.LoadSceneAsync(SceneName.GAME);
        }

        private void OnLeave()
        {
            _audioProvider.Play(_audioName.UIClickSound);
            Application.Quit();
        }
    }
}