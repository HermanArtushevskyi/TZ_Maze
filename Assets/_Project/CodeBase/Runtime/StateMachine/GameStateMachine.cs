using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.States.GameStates;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Cysharp.Threading.Tasks;
using InputService.Common;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.StateMachine
{
    public class GameStateMachine : StateMachineBase
    {
        public GameStateMachine(
            ICurtain curtain,
            ITimer timer,
            IUpdate update,
            IEnemyProvider enemyProvider,
            IFactory<GameObject, GameObject, Transform> playerFactory,
            IFactory<IView, GameObject> uiFactory,
            IFactory<ILevel> levelFactory,
            IFactory<IEnemy> enemyFactory,
            GameUIActions uiActions,
            IInputProvider inputProvider,
            InputActions inputActions,
            ICharacterController characterController,
            [InjectOptional(Id = StateMachineId.Enemy)] IStateMachine _enemyStateMachine,
            [InjectOptional(Id = PrefabId.UI)] GameObject uiPrefab,
            [InjectOptional(Id = PrefabId.Player)] GameObject playerPrefab)
        {
            States = new Dictionary<Type, IExitableState>();
            States.Add(typeof(JustLoadedState), new JustLoadedState(curtain, playerFactory, uiFactory, levelFactory,
                enemyFactory, _enemyStateMachine, inputProvider, inputActions, uiPrefab, playerPrefab, this));
            States.Add(typeof(GameLoopState), new GameLoopState(timer, update, uiActions, characterController, this, enemyProvider, _enemyStateMachine));
            States.Add(typeof(GameLostState), new GameLostState(uiActions));
            States.Add(typeof(GameWonState), new GameWonState(uiActions));
        }

        public override async UniTask Initialize()
        {
            await Enter<JustLoadedState>();
        }
    }
}