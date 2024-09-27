using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Services.SceneService.Interfaces;
using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;
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
            [InjectOptional(Id = PrefabId.UI)] GameObject uiPrefab,
            [InjectOptional(Id = PrefabId.Player)] GameObject playerPrefab)
        {
            States = new Dictionary<Type, IExitableState>();
            States.Add(typeof(JustLoadedState), new JustLoadedState(curtain, playerFactory, uiFactory, uiPrefab, playerPrefab, this));
            States.Add(typeof(GameStartedState), new GameStartedState(timer, update));
        }

        public override async UniTask Initialize()
        {
            await Enter<JustLoadedState>();
        }
    }
}