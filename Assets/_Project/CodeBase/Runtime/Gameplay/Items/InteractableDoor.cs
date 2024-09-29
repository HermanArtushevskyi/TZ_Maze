using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Common;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Items
{
    public class InteractableDoor : InteractableBase
    {
        private IKeyCounter _keyCounter;
        private GameUIActions _uiActions;
        private IAudioProvider _audioProvider;
        private AudioName _audioName;

        [Inject]
        private void GetDependencies(IKeyCounter keyCounter, GameUIActions gameUIActions, IAudioProvider audioProvider,
            AudioName audioName)
        {
            _keyCounter = keyCounter;
            _uiActions = gameUIActions;
            _audioProvider = audioProvider;
            _audioName = audioName;
        }
        
        public override void Interact()
        {
            if (_keyCounter.GetAmountOfGatheredKeys() == _keyCounter.GetAmountOfNeededKeys())
            {
                _audioProvider.Play(_audioName.UnlockedDoorSound);
                _uiActions.CallWin();
            }
            else
            {
                _audioProvider.Play(_audioName.LockedDoorSound);
            }
        }
    }
}