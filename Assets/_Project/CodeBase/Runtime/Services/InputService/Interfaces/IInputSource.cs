using InputService.Common;

namespace _Project.CodeBase.Runtime.Services.InputService.Interfaces
{
    public interface IInputSource
    {
        public int Priority { get; }
        public void GetInput(in RawInput input);
    }
}