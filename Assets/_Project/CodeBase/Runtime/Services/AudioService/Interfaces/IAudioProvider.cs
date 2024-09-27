using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.AudioService.Interfaces
{
    public interface IAudioProvider
    {
        public IAudioAsset Play(string audioName, GameObject emitter = null);
    }
}