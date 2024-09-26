using System.Collections.Generic;
using InputService.Common;

namespace _Project.CodeBase.Runtime.Services.InputService.Interfaces
{
    public interface IInputProvider
    {
        public List<IInputSource> Sources { get; }
        public RawInput GetInput();
        public void AddSource(IInputSource source);
        public void RemoveSource(IInputSource source);
    }
}