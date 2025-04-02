using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace eyesGuard.Services
{
    public enum LogLevel
    {
        Debug,
        Information,
        Warning,
        Error
    }

    public class LogService
    {
        private readonly string logDirectory;
        private readonly string logFilePath;
        private const int MaxLogDays = 7;
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB
        private LogLevel minimumLevel = LogLevel.Information;

        public LogService()
        {
            logDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "EyesGuard",
                "Logs");

            Directory.CreateDirectory(logDirectory);
            logFilePath = Path.Combine(logDirectory, $"log_{DateTime.Now:yyyy-MM-dd}.txt");
            CleanupOldLogs();
        }

        public LogLevel MinimumLevel
        {
            get => minimumLevel;
            set => minimumLevel = value;
        }

        public void LogDebug(string message)
        {
            if (minimumLevel <= LogLevel.Debug)
                WriteLog("DEBUG", message);
        }

        public void LogInformation(string message)
        {
            if (minimumLevel <= LogLevel.Information)
                WriteLog("INFO", message);
        }

        public void LogWarning(string message)
        {
            if (minimumLevel <= LogLevel.Warning)
                WriteLog("WARN", message);
        }

        public void LogError(string message, Exception? ex = null)
        {
            if (minimumLevel <= LogLevel.Error)
            {
                string errorMessage = ex != null 
                    ? $"{message} - Exception: {ex.Message}\nStackTrace: {ex.StackTrace}"
                    : message;
                WriteLog("ERROR", errorMessage);
            }
        }

        private void WriteLog(string level, string message)
        {
            CheckFileSize();
            try
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}\n";
                File.AppendAllText(logFilePath, logEntry);
            }
            catch
            {
                // 如果写入日志失败，我们不能做太多事情，因为这本身就是错误处理机制
            }
        }

        private void CheckFileSize()
        {
            try
            {
                var fileInfo = new FileInfo(logFilePath);
                if (fileInfo.Exists && fileInfo.Length >= MaxFileSize)
                {
                    string archivePath = Path.Combine(logDirectory, 
                        $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
                    File.Move(logFilePath, archivePath);
                }
            }
            catch
            {
                // 如果检查文件大小失败，继续使用当前文件
            }
        }

        private void CleanupOldLogs()
        {
            try
            {
                var logFiles = Directory.GetFiles(logDirectory, "log_*.txt")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.CreationTime)
                    .Skip(MaxLogDays);

                foreach (var file in logFiles)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch
                    {
                        // 忽略单个文件删除失败
                    }
                }
            }
            catch
            {
                // 如果清理失败，继续运行程序
            }
        }
    }
}