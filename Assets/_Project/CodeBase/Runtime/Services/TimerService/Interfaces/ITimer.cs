using System;

namespace _Project.CodeBase.Runtime.Services.TimerService.Interfaces
{
    public interface ITimer
    {
        /// <summary>
        /// Starts the local timer.
        /// </summary>
        /// <returns>Timer id. 0 is timer that started with app.</returns>
        public int StartTimer();
        public void StopTimer(int timerId);
        public void PauseTimer(int timerId);
        public void ResumeTimer(int timerId);
        public void ResetTimer(int timerId);
        public TimeSpan GetTime(int timerId);
    }
}