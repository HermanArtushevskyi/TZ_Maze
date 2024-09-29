using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Items
{
    public class InteractableKey : InteractableBase
    {
        private IKeyCounter _keyCounter;
        private IAudioProvider _audioProvider;
        private AudioName _audioName;

        [Inject]
        private void GetDependencies(IKeyCounter keyCounter, IAudioProvider audioProvider, AudioName audioName)
        {
            _keyCounter = keyCounter;
            _audioProvider = audioProvider;
            _audioName = audioName;
        }
        
        public override void Interact()
        {
            _keyCounter.AddKey();
            _audioProvider.Play(_audioName.KeyFoundSound);
            Destroy(gameObject);
        }
    }
}