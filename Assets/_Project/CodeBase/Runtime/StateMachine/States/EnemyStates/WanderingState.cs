using System.Collections.Generic;
using _Project.CodeBase.Runtime.Common;
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
    public class WanderingState : EnemyBaseState, IStateWithPayload<Transform>
    {
        private readonly EnemyConfig _enemyConfig;
        private readonly IEnemyProvider _enemyProvider;
        private readonly IPlayer _player;
        private readonly IUpdate _update;
        private readonly ICharacterController _characterController;
        private readonly IStateMachine _stateMachine;

        private AABB _playerAABB;
        private Frustum _frustum;

        private const float WalkPitch = 1f;
        
        public WanderingState(EnemyConfig enemyConfig,
            IEnemyProvider enemyProvider,
            List<IPlaceOfInterest> places,
            IPlayer player,
            IUpdate update,
            ICharacterController characterController,
            IStateMachine stateMachine) : base(places)
        {
            _enemyConfig = enemyConfig;
            _enemyProvider = enemyProvider;
            _player = player;
            _update = update;
            _characterController = characterController;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter(Transform destination)
        {
#if UNITY_EDITOR
            Debug.Log($"Enemy is going to {destination.position}");
#endif
            Footsteps.SetParameter("Pitch", WalkPitch);
            _enemyProvider.GetEnemy().MoveTo(destination);
            _update.OnUpdate += Update;
            Application.quitting += Quitting;
            await UniTask.CompletedTask;
        }

        public async UniTask Exit()
        {
            _update.OnUpdate -= Update;
            Application.quitting -= Quitting;
            await UniTask.CompletedTask;
        }

        private async void Update()
        {
            IEnemy enemy = _enemyProvider.GetEnemy();

            if (enemy.CanMove == false)
            {
                await _stateMachine.Enter<EnemyIdleState>();
                return;
            }
            
            Vector3 enemyHeadPosition = enemy.EnemyOnScene.transform.position + Vector3.up * 2f;
            _frustum = new Frustum(_enemyConfig.FOV, _enemyConfig.ViewDistance, 0.3f, 0.5f, Vector3.up,
                enemy.EnemyOnScene.transform.right, enemy.EnemyOnScene.transform.forward, enemyHeadPosition);
            _frustum.NearPlane.Flip();
            
            bool isPlayerInSight = MyMath.IsAABBIntersectingFrustum(_frustum, _characterController.PlayerAABB);
            if (enemy.NavMeshAgent.remainingDistance < 1f)
            {
                enemy.NavMeshAgent.SetDestination(GetRandomPlace().position);
            }
            if (isPlayerInSight)
            {
                await _stateMachine.Enter<ChaseState>();
            }
#if UNITY_EDITOR
            Debug.DrawLine(enemy.EnemyOnScene.transform.position, enemy.EnemyOnScene.transform.position + enemy.EnemyOnScene.transform.forward * _enemyConfig.ViewDistance, Color.green);
            // Draw player detection frustum
            Color frustumColor = isPlayerInSight ? Color.green : Color.red;
            MyMath.DrawFrustum(_frustum, frustumColor);
#endif
        }

        private async void Quitting()
        {
            await _stateMachine.Shutdown();
        }
    }
}