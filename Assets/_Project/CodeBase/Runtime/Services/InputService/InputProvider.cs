using System.Collections.Generic;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using InputService.Common;

namespace _Project.CodeBase.Runtime.Services.InputService
{
    public class InputProvider : IInputProvider
    {
        public List<IInputSource> Sources { get; }
        
        private readonly RawInput _input;
        
        public InputProvider()
        {
            Sources = new List<IInputSource>();
            _input = new RawInput();
        }
        
        public RawInput GetInput()
        {
            foreach (IInputSource source in Sources)
            {
                source.GetInput(in _input);
            }
            
            return _input;
        }

        public void AddSource(IInputSource source)
        {
            Sources.Add(source);
            Sources.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }

        public void RemoveSource(IInputSource source)
        {
            Sources.Remove(source);
        }
    }
}