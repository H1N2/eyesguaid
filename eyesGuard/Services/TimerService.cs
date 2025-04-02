using System;

namespace eyesGuard.Services
{
    public class TimerService : IDisposable
    {
        public bool IsRunning { get; private set; }
        public TimeSpan Elapsed { get; private set; }
        public TimeSpan TodayWorkDuration { get; private set; }

        private DateTime _startTime;
        private bool _disposed;

        public void Start()
        {
            if (!IsRunning)
            {
                _startTime = DateTime.Now;
                IsRunning = true;
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                Elapsed += DateTime.Now - _startTime;
                TodayWorkDuration += DateTime.Now - _startTime;
                IsRunning = false;
            }
        }

        public void Reset()
        {
            Elapsed = TimeSpan.Zero;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                }
                _disposed = true;
            }
        }
    }
}