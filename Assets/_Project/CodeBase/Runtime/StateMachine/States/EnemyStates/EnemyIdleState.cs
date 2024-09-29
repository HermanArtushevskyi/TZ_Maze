using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine.States.EnemyStates
{
    public class EnemyIdleState : EnemyBaseState, IState
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly IUpdate _update;
        private readonly IStateMachine _stateMachine;

        public EnemyIdleState(List<IPlaceOfInterest> placeOfInterests,
            IEnemyProvider enemyProvider,
            IUpdate update,
            IStateMachine stateMachine) : base(placeOfInterests)
        {
            _enemyProvider = enemyProvider;
            _update = update;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            Footsteps.Pause();
            _enemyProvider.GetEnemy().NavMeshAgent.isStopped = true;
            _update.OnUpdate += Update;
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            Footsteps.Unpause();
            _update.OnUpdate -= Update;
            await UniTask.CompletedTask;
        }

        private async void Update()
        {
            IEnemy enemy = _enemyProvider.GetEnemy();
            if (enemy.CanMove)
            {
                enemy.NavMeshAgent.isStopped = false;
                await _stateMachine.Enter<WanderingState, Transform>(GetRandomPlace());
            }
        }
    }
}