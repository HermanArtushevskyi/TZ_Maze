using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Traps
{
    [RequireComponent(typeof(Collider))]
    public class SpikeTrap : TrapBase
    {
        private IAudioProvider _audioProvider;
        private AudioName _audioName;

        [Inject]
        private void GetDependencies(IAudioProvider audioProvider, AudioName audioName)
        {
            _audioProvider = audioProvider;
            _audioName = audioName;
        }
        
        public override string GetDeathMessage() => "You were impaled by spikes!";
        public override void Trigger()
        {
            _audioProvider.Play(_audioName.SpikeTrapSound, gameObject);
        }
    }
}