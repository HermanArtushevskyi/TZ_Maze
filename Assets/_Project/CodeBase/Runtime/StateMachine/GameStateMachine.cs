using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Levels.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;
using _Project.CodeBase.Runtime.Services.UIService.Common;
using _Project.CodeBase.Runtime.Services.UIService.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using _Project.CodeBase.Runtime.StateMachine.States.GameStates;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Cysharp.Threading.Tasks;
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
            IFactory<GameObject, GameObject, Transform> playerFactory,
            IFactory<IView, GameObject> uiFactory,
            IFactory<ILevel> levelFactory,
            GameUIActions uiActions,
            ICharacterController characterController,
            [InjectOptional(Id = PrefabId.UI)] GameObject uiPrefab,
            [InjectOptional(Id = PrefabId.Player)] GameObject playerPrefab)
        {
            States = new Dictionary<Type, IExitableState>();
            States.Add(typeof(JustLoadedState), new JustLoadedState(curtain, playerFactory, uiFactory, levelFactory, uiPrefab, playerPrefab, this));
            States.Add(typeof(GameLoopState), new GameLoopState(timer, update, uiActions, characterController, this));
            States.Add(typeof(GameLostState), new GameLostState(uiActions));
            States.Add(typeof(GameWonState), new GameWonState(uiActions));
        }

        public override async UniTask Initialize()
        {
            await Enter<JustLoadedState>();
        }
    }
}