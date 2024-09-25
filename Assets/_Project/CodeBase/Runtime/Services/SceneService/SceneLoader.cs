using System;
using System.Threading;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using Cysharp.Threading.Tasks;
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

        public UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, bool autoActivate = true,
            IProgress<float> progress = null, CancellationToken cancellationToken = default)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, mode);
            asyncOperation.allowSceneActivation = autoActivate;
            asyncOperation.completed += operation => OnSceneLoaded?.Invoke(sceneName);
            UniTask uniTask = asyncOperation.ToUniTask(progress: progress, cancellationToken: cancellationToken);
            return uniTask;
        }

        public UniTask UnloadSceneAsync(string sceneName, IProgress<float> progress = null, CancellationToken cancellationToken = default)
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            asyncOperation.completed += operation => OnSceneUnloaded?.Invoke(sceneName);
            UniTask uniTask = asyncOperation.ToUniTask(progress: progress, cancellationToken: cancellationToken);
            return uniTask;
        }
    }
}