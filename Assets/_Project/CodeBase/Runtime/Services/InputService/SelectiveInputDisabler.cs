using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using InputService.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Runtime.Services.InputService
{
    public class SelectiveInputDisabler : IInputSource
    {
        private readonly DisabledInput _disabledInput;

        public SelectiveInputDisabler(DisabledInput disabledInput)
        {
            _disabledInput = disabledInput;
        }

        public int Priority => 2;
        public void GetInput(in RawInput input)
        {
            if (_disabledInput.MovementInput)
                input.MovementDirection = Vector2.zero;
            if (_disabledInput.RunningInput)
                input.IsRunning = false;
            if (_disabledInput.MouseDeltaInput)
                input.MouseDelta = Vector2.zero;
            if (_disabledInput.ActionInput)
                input.IsActionClicked = false;
            if (_disabledInput.AttackInput)
                input.IsAttackClicked = false;
        }

        public struct DisabledInput
        {
            public bool MovementInput;
            public bool RunningInput;
            public bool MouseDeltaInput;
            public bool ActionInput;
            public bool AttackInput;
        }
    }
}