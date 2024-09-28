namespace _Project.CodeBase.Runtime.Services.AudioService.Interfaces
{
    public interface IAudioAsset
    {
        public string Name { get; }

        public void Play();
        public void Pause();
        public void Unpause();
        public void Stop();
        public void Kill();
        
        public void SetParameter(string name, float value);
    }
}