using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Entrypoints
{
    public class AppEntrypoint : MonoBehaviour
    {
        private IStateMachine _stateMachine;

        [Inject]
        private void GetDependencies(
            [InjectOptional(Id = StateMachineId.App)] IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private async void Start()
        {
            await _stateMachine.Initialize();
        }
    }
}