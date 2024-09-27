using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.CodeBase.Runtime.Services.SceneService.Interfaces
{
    public interface ISceneLoader
    {
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        public UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, IProgress<float> progress = null, CancellationToken cancellationToken = default);
        public UniTask UnloadSceneAsync(string sceneName, IProgress<float> progress = null, CancellationToken cancellationToken = default);
    }
}