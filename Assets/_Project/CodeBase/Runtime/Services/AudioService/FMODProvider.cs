using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.AudioService
{
    public class FMODProvider : IAudioProvider
    {
        public IAudioAsset Play(string audioName, GameObject emitter = null)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(audioName);
            if (emitter != null)
            {
                RuntimeManager.AttachInstanceToGameObject(eventInstance, emitter.transform,
                    emitter.GetComponent<Rigidbody>());
            }
            eventInstance.start();
            return new FMODAsset(eventInstance, audioName);
        }
    }
}