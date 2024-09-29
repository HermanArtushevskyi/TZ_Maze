using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine.States.EnemyStates
{
    public class SpawnedState : EnemyBaseState, IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IAudioProvider _audioProvider;
        private readonly AudioName _audioName;
        private readonly IEnemyProvider _enemyProvider;

        public SpawnedState(
            IStateMachine stateMachine,
            IAudioProvider audioProvider,
            AudioName audioName,
            IEnemyProvider enemyProvider,
            List<IPlaceOfInterest> placeOfInterests) : base(placeOfInterests)
        {
            _stateMachine = stateMachine;
            _audioProvider = audioProvider;
            _audioName = audioName;
            _enemyProvider = enemyProvider;
        }


        public async UniTask Enter()
        {
            Footsteps = _audioProvider.Play(_audioName.EnemyFootstepsSound, _enemyProvider.GetEnemy().EnemyOnScene);
            await _stateMachine.Enter<WanderingState, Transform>(GetRandomPlace());
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
        }
    }
}