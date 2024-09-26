using UnityEngine;

namespace InputService.Common
{
    public class RawInput
    {
        public Vector2 MovementDirection;
        public bool IsRunning;
        public Vector2 MousePos;
        public Vector2 MouseDelta;
        public bool IsActionClicked;
        public bool IsAttackClicked;
    }
}