using NUnit.Framework;
using eyesGuard.Services;
using System;
using System.IO;

namespace eyesGuard.Core.Tests.Services
{
    [TestFixture]
    public class LogServiceTests
    {
        private LogService _logService;
        private string _testLogDirectory;

        [SetUp]
        public void Setup()
        {
            _testLogDirectory = Path.Combine(Path.GetTempPath(), "EyesGuardTestLogs");
            _logService = new LogService();
        }

        [TearDown]
        public void Cleanup()
        {
            try
            {
                if (Directory.Exists(_testLogDirectory))
                    Directory.Delete(_testLogDirectory, true);
            }
            catch { }
        }

        [Test]
        public void LogDebug_WhenMinimumLevelIsDebug_ShouldWriteLog()
        {
            _logService.MinimumLevel = LogLevel.Debug;
            _logService.LogDebug("Test debug message");
            
            Assert.IsTrue(File.Exists(_logService.LogFilePath));
        }

        [Test]
        public void LogInformation_WhenMinimumLevelIsInformation_ShouldWriteLog()
        {
            _logService.MinimumLevel = LogLevel.Information;
            _logService.LogInformation("Test info message");
            
            Assert.IsTrue(File.Exists(_logService.LogFilePath));
        }

        [Test]
        public void LogError_WithException_ShouldIncludeExceptionDetails()
        {
            try
            {
                throw new Exception("Test exception");
            }
            catch (Exception ex)
            {
                _logService.LogError("Test error", ex);
                
                var logContent = File.ReadAllText(_logService.LogFilePath);
                Assert.IsTrue(logContent.Contains("Test exception"));
            }
        }

        [Test]
        public void CheckFileSize_WhenExceedsMaxSize_ShouldArchiveLog()
        {
            // 模拟大文件
            File.WriteAllText(_logService.LogFilePath, new string('a', 11 * 1024 * 1024));
            
            _logService.LogInformation("Test message");
            
            Assert.IsTrue(Directory.GetFiles(_testLogDirectory, "log_*.txt").Length > 1);
        }
    }
}