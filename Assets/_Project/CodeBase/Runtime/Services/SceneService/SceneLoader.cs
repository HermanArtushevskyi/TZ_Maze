using System;
using System.Threading;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Runtime.Services.SceneService
{
    public class SceneLoader : ISceneLoader, ISceneEvents
    {
        public event Action<string> OnSceneLoaded;
        public event Action<string> OnSceneUnloaded;
        
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, mode);
            OnSceneLoaded?.Invoke(sceneName);
        }

        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single,
            IProgress<float> progress = null, CancellationToken cancellationToken = default)
        {
            UniTask uniTask = SceneManager.LoadSceneAsync(sceneName, mode).ToUniTask(progress: progress, cancellationToken: cancellationToken);
            await uniTask;
        }

        public async UniTask UnloadSceneAsync(string sceneName, IProgress<float> progress = null, CancellationToken cancellationToken = default)
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            asyncOperation.completed += operation => OnSceneUnloaded?.Invoke(sceneName);
            UniTask uniTask = asyncOperation.ToUniTask(progress: progress, cancellationToken: cancellationToken);
            await uniTask;
        }
    }
}