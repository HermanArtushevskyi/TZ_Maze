using System;

namespace _Project.CodeBase.Runtime.Services.SceneService.Interfaces
{
    public interface ISceneEvents
    {
        public event Action<string> OnSceneLoaded;
        public event Action<string> OnSceneUnloaded;
    }
}