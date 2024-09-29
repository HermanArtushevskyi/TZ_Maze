using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Entrypoints
{
    public class GameEntrypoint : MonoBehaviour
    {
        
        private IStateMachine _stateMachine;
        private IStateMachine _enemyStateMachine;
        private IAudioProvider _audioProvider;
        private AudioName _audioName;
        
        private IAudioAsset _ambientAudio;

        [Inject]
        private void GetDependencies(
            [InjectOptional(Id = StateMachineId.Game)] IStateMachine stateMachine,
            [InjectOptional(Id = StateMachineId.Enemy)] IStateMachine enemyStateMachine,
            IAudioProvider audioProvider, AudioName audioName)
        {
            _stateMachine = stateMachine;
            _enemyStateMachine = enemyStateMachine;
            _audioProvider = audioProvider;
            _audioName = audioName;
        }
        
        private async void Start()
        {
            _ambientAudio = _audioProvider.Play(_audioName.AmbientSound);
            await _stateMachine.Initialize();
        }
        
        private void OnDestroy()
        {
            _ambientAudio.Kill();
        }
    }
}