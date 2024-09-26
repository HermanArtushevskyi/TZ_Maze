using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using InputService.Common;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.InputService
{
    public class UnityInputSource : IInputSource
    {
        public int Priority => 1;
        private readonly InputActions _inputActions;
        
        public UnityInputSource()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
        }
        
        public void GetInput(in RawInput rawInput)
        {
            rawInput.MovementDirection = _inputActions.Player.Movement.ReadValue<Vector2>();
            rawInput.IsRunning = _inputActions.Player.Acceleration.ReadValue<float>() > 0.5f;
            rawInput.MousePos = _inputActions.Player.MousePos.ReadValue<Vector2>();
            rawInput.MouseDelta = _inputActions.Player.MouseDelta.ReadValue<Vector2>();
            rawInput.IsActionClicked = _inputActions.Player.Action.triggered;
            rawInput.IsAttackClicked = _inputActions.Player.Attack.triggered;
        }
        
        ~UnityInputSource()
        {
            _inputActions.Disable();
        }
    }
}