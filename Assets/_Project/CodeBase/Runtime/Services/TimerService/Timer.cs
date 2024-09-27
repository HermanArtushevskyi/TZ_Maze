using System;
using System.Collections.Generic;
using System.Diagnostics;
using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;

namespace _Project.CodeBase.Runtime.Services.TimerService
{
    public class Timer : ITimer
    {
        private readonly List<Stopwatch> _timers = new List<Stopwatch>();
        
        public int StartTimer()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            _timers.Add(timer);
            return _timers.Count - 1;
        }

        public void StopTimer(int timerId)
        {
            _timers[timerId].Stop();
        }

        public void PauseTimer(int timerId)
        {
            _timers[timerId].Stop();
        }

        public void ResumeTimer(int timerId)
        {
            _timers[timerId].Start();
        }

        public void ResetTimer(int timerId)
        {
            _timers[timerId].Reset();
        }

        public TimeSpan GetTime(int timerId)
        {
            return _timers[timerId].Elapsed;
        }
    }
}