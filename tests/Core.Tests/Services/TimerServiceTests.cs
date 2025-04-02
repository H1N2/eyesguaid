using NUnit.Framework;
using eyesGuard.Services;
using System;

namespace eyesGuard.Core.Tests.Services
{
    [TestFixture]
    public class TimerServiceTests
    {
        private TimerService _timerService;

        [SetUp]
        public void Setup()
        {
            _timerService = new TimerService();
        }

        [TearDown]
        public void Cleanup()
        {
            _timerService.Dispose();
        }

        [Test]
        public void Start_ShouldSetIsRunningToTrue()
        {
            _timerService.Start();
            Assert.IsTrue(_timerService.IsRunning);
        }

        [Test]
        public void Stop_ShouldSetIsRunningToFalse()
        {
            _timerService.Start();
            _timerService.Stop();
            Assert.IsFalse(_timerService.IsRunning);
        }

        [Test]
        public void Reset_ShouldResetElapsedTime()
        {
            _timerService.Start();
            System.Threading.Thread.Sleep(100);
            _timerService.Reset();
            Assert.AreEqual(TimeSpan.Zero, _timerService.Elapsed);
        }

        [Test]
        public void TodayWorkDuration_ShouldReturnCorrectValue()
        {
            _timerService.Start();
            System.Threading.Thread.Sleep(100);
            _timerService.Stop();
            Assert.Greater(_timerService.TodayWorkDuration, TimeSpan.Zero);
        }
    }
}