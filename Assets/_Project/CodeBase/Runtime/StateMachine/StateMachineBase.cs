using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Runtime.StateMachine
{
    public abstract class StateMachineBase : IStateMachine
    {
        public IExitableState CurrentState { get; set; }
        protected Dictionary<Type, IExitableState> States { get; set; }

        public abstract UniTask Initialize();

        public virtual UniTask<TransitionResult> Enter<TState>() where TState : IState
        {
            if (typeof(TState) == CurrentState?.GetType())
                return UniTask.FromResult(TransitionResult.Invalid());
            
            IState requestedState;
            if (States.ContainsKey(typeof(TState)))
            {
                requestedState = (IState) States[typeof(TState)];
            }
            else return UniTask.FromResult(TransitionResult.Invalid());

            CurrentState?.Exit();
            requestedState.Enter();
            CurrentState = requestedState;
            return UniTask.FromResult(TransitionResult.Valid());
        }

        public virtual UniTask<TransitionResult> Enter<TState, TPayload>(TPayload payload) where TState : IStateWithPayload<TPayload>
        {
            if (typeof(TState) == CurrentState?.GetType())
                return UniTask.FromResult(TransitionResult.Invalid());
            
            IStateWithPayload<TPayload> requestedState;
            if (States.ContainsKey(typeof(TState)))
            {
                requestedState = (IStateWithPayload<TPayload>) States[typeof(TState)];
            }
            else return UniTask.FromResult(TransitionResult.Invalid());

            CurrentState?.Exit();
            requestedState.Enter(payload);
            CurrentState = requestedState;
            return UniTask.FromResult(TransitionResult.Valid());
        }
    }
}