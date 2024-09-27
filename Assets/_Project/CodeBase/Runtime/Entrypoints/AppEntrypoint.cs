using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Entrypoints
{
    public class AppEntrypoint : MonoBehaviour
    {
        private IStateMachine _stateMachine;
        private ITimer _timer;

        [Inject]
        private void GetDependencies(
            [InjectOptional(Id = StateMachineId.App)] IStateMachine stateMachine,
            ITimer timer)
        {
            _stateMachine = stateMachine;
            _timer = timer;
        }
        
        private async void Start()
        {
            int appTimerId = _timer.StartTimer();
            if (appTimerId != 0)
                Debug.LogWarning("App timer id is not 0");
            await _stateMachine.Initialize();
        }
    }
}