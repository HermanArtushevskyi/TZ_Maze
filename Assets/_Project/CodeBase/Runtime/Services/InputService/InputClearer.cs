using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using InputService.Common;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Services.InputService
{
    public class InputClearer : IInputSource
    {
        public int Priority => -1;
        public void GetInput(in RawInput input)
        {
            input.MovementDirection = Vector2.zero;
            input.IsRunning = false;
            input.MousePos = Vector2.zero;
            input.MouseDelta = Vector2.zero;
            input.IsActionClicked = false;
            input.IsAttackClicked = false;
        }
    }
}