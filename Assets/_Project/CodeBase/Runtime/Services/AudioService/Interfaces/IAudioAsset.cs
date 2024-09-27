namespace _Project.CodeBase.Runtime.Services.AudioService.Interfaces
{
    public interface IAudioAsset
    {
        public string Name { get; }

        public void Play();
        public void Stop();
        public void Kill();
    }
}