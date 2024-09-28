using System;
using System.Collections.Generic;
using System.Diagnostics;
using _Project.CodeBase.Runtime.Services.TimerService.Interfaces;

namespace _Project.CodeBase.Runtime.Services.TimerService
{
    public class Timer : ITimer
    {
        private readonly List<Stopwatch> _timers = new List<Stopwatch>();
        private readonly Queue<int> _freeIds = new Queue<int>();
        
        public int StartTimer()
        {
            if (_freeIds.Count > 0)
            {
                int freeId = _freeIds.Dequeue();
                _timers[freeId].Start();
                return freeId;
            }
            
            Stopwatch timer = new Stopwatch();
            timer.Start();
            _timers.Add(timer);
            return _timers.Count - 1;
        }

        public void StopTimer(int timerId)
        {
            _timers[timerId].Stop();
            _timers[timerId].Reset();
            _freeIds.Enqueue(timerId);
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