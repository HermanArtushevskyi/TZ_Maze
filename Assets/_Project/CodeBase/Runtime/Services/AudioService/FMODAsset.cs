using _Project.CodeBase.Runtime.Services.AudioService.Interfaces;
using FMOD.Studio;

namespace _Project.CodeBase.Runtime.Services.AudioService
{
    public class FMODAsset : IAudioAsset
    {
        public string Name { get; }
        
        private readonly EventInstance _eventInstance;

        public FMODAsset(EventInstance eventInstance, string name)
        {
            _eventInstance = eventInstance;
            Name = name;
        }

        public void Play()
        {
            _eventInstance.start();
        }

        public void Pause()
        {
            _eventInstance.setPaused(true);
        }

        public void Unpause()
        {
            _eventInstance.setPaused(false);
        }

        public void Stop()
        {
            _eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        public void Kill()
        {
            _eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        public void SetParameter(string name, float value)
        {
            if (name == "Pitch") _eventInstance.setPitch(value);

            else
                _eventInstance.setParameterByName(name, value);
        }
    }
}