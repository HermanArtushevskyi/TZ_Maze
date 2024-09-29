using System;
using _Project.CodeBase.Runtime.Services.SceneService.Common;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine.States.AppStates
{
    public class MainLoopState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        
        public MainLoopState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter()
        {
            IProgress<float> sceneLoadingProgress = null;
            if (Application.isEditor)
                sceneLoadingProgress = new Progress<float>(ProgressHandler);
            await _sceneLoader.LoadSceneAsync(SceneName.MAIN_MENU, progress: sceneLoadingProgress);
            await UniTask.CompletedTask;
        }

        public async UniTask Exit() => await UniTask.CompletedTask;

        private void ProgressHandler(float obj)
        {
            Debug.Log($"Scene loading progress: {obj}");
        }
    }
}