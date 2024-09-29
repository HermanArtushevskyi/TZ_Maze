using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Runtime.StateMachine.States.EnemyStates
{
    public class ChaseState : EnemyBaseState, IState
    {
        private readonly EnemyConfig _enemyConfig;
        private readonly IEnemyProvider _enemyProvider;
        private readonly IPlayer _player;
        private readonly IUpdate _update;
        private readonly EnemyStateMachine _stateMachine;

        private const float RunPitch = 1.5f;

        public ChaseState(EnemyConfig enemyConfig, IEnemyProvider enemyProvider,
            List<IPlaceOfInterest> places,
            IPlayer player, IUpdate update, EnemyStateMachine stateMachine) : base(places)
        {
            _enemyConfig = enemyConfig;
            _enemyProvider = enemyProvider;
            _player = player;
            _update = update;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
#if UNITY_EDITOR
            Debug.Log("Enemy is chasing player");
#endif
            Footsteps.SetParameter("Pitch", RunPitch);
            _enemyProvider.GetEnemy().MoveTo(_player.SceneObject.transform, true);
            _update.OnUpdate += OnUpdate;
            Application.quitting += Quitting;
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            _update.OnUpdate -= OnUpdate;
            Application.quitting -= Quitting;
            await UniTask.CompletedTask;
        }

        private async void OnUpdate()
        {
            IEnemy enemy = _enemyProvider.GetEnemy();
            if (enemy.EnemyOnScene == null) _update.OnUpdate -= OnUpdate;

            if (enemy.CanMove == false)
            {
                await _stateMachine.Enter<EnemyIdleState>();
                return;
            }
            
            Vector3 enemy2DPos = enemy.EnemyOnScene.transform.position;
            enemy2DPos.y = 0;
            Vector3 player2DPos = _player.SceneObject.transform.position;
            player2DPos.y = 0;

            float playerToEnemyDistance = (enemy2DPos - player2DPos).sqrMagnitude;

            enemy.MoveTo(_player.SceneObject.transform, true);
            
            if (playerToEnemyDistance > _enemyConfig.ChaseDistance * _enemyConfig.ChaseDistance)
            {
                await _stateMachine.Enter<WanderingState, Transform>(GetRandomPlace());
            }
        }

        private async void Quitting()
        {
            await _stateMachine.Shutdown();
        }
    }
}