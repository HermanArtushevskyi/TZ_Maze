using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.Services.AudioService.Common;
using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.States.EnemyStates;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine
{
    public class EnemyStateMachine : StateMachineBase
    {
        public EnemyStateMachine(
            EnemyConfig enemyConfig,
            IEnemyProvider enemyProvider,
            ICharacterController characterController,
            IPlayer player,
            IAudioProvider audioProvider,
            AudioName audioName,
            List<IPlaceOfInterest> places,
            IUpdate update)
        {
            States = new Dictionary<Type, IExitableState>();
            States.Add(typeof(SpawnedState) , new SpawnedState(this, audioProvider, audioName, enemyProvider, places));
            States.Add(typeof(WanderingState) , new WanderingState(enemyConfig, enemyProvider, places, player, update, characterController, this));
            States.Add(typeof(ChaseState), new ChaseState(enemyConfig, enemyProvider, places, player, update, this));
            States.Add(typeof(EnemyIdleState), new EnemyIdleState(places, enemyProvider, update, this));
        }

        public override async UniTask Initialize()
        {
            await Enter<SpawnedState>();
        }
    }
}