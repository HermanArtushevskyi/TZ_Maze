using _Project.CodeBase.Runtime.StateMachine;
using _Project.CodeBase.Runtime.StateMachine.Common;
using _Project.CodeBase.Runtime.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

namespace _Project.CodeBase.Tests.StateMachine
{
    public class StateMachineTests
    {
        private IStateMachine _stateMachine;
        
        [SetUp]
        public void Setup()
        {
            _stateMachine = new TestStateMachine();
        }
        
        /// <summary>
        /// Testing initialization of the state machine.
        /// If the state machine is initialized, the first state should be entered.
        /// </summary>
        [Test]
        public async void Test_initialization()
        {
            await _stateMachine.Initialize();
            Assert.AreEqual(typeof(TestState1), _stateMachine.CurrentState.GetType());
        }

        /// <summary>
        /// Testing transition between states.
        /// If transition is requested, the new state should be entered.
        /// </summary>
        [Test]
        public async void Test_transition()
        {
            await _stateMachine.Initialize();
            TransitionResult result = await _stateMachine.Enter<TestState2>();
            Assert.AreEqual(TransitionResult.Valid(), result);
            Assert.AreEqual(typeof(TestState2), _stateMachine.CurrentState.GetType());
        }
        
        /// <summary>
        /// Testing transition between states with payload.
        /// If transition is done, the state must have the payload.
        /// </summary>
        [Test]
        public async void Test_transition_with_payload()
        {
            await _stateMachine.Initialize();
            TransitionResult result = await _stateMachine.Enter<PayloadedTestState, int>(42);
            Assert.AreEqual(TransitionResult.Valid(), result);
            Assert.AreEqual(typeof(PayloadedTestState), _stateMachine.CurrentState.GetType());
            Assert.AreEqual(42, ((PayloadedTestState) _stateMachine.CurrentState).Payload);
        }
        
        /// <summary>
        /// Testing invalid transition.
        /// If transition is invalid, the current state should not change.
        /// </summary>
        [Test]
        public async void Test_invalid_transition()
        {
            await _stateMachine.Initialize();
            TransitionResult result = await _stateMachine.Enter<TestState3>();
            Assert.AreEqual(TransitionResult.Invalid(), result);
            Assert.AreEqual(typeof(TestState1), _stateMachine.CurrentState.GetType());
        }
    }

    internal class TestStateMachine : StateMachineBase
    {
        public TestStateMachine()
        {
            States = new();
            States.Add(typeof(TestState1), new TestState1());
            States.Add(typeof(TestState2), new TestState2());
            States.Add(typeof(PayloadedTestState), new PayloadedTestState());
        }

        public override UniTask Initialize()
        {
            Enter<TestState1>();
            return UniTask.CompletedTask;
        }
    }
    
    internal struct TestState1 : IState
    {
        public UniTask Enter()
        {
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
    
    internal struct TestState2 : IState
    {
        public UniTask Enter()
        {
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
    
    internal struct TestState3 : IState
    {
        public UniTask Enter()
        {
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
    
    internal struct PayloadedTestState : IStateWithPayload<int>
    {
        public int Payload { get; private set; }
        
        public UniTask Enter(int payload)
        {
            Payload = payload;
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}