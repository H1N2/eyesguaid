using System;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;
using eyesGuard.Services;

namespace eyesGuard
{
    public class Config : INotifyPropertyChanged
    {
        private static LogService logService = new LogService();
        public event PropertyChangedEventHandler? PropertyChanged;
        private int workMinutes = 25;
        private int restMinutes = 5;

        public int WorkMinutes
        {
            get => workMinutes;
            set
            {
                if (value < 1) throw new ArgumentException("工作时间必须大于0分钟");
                if (workMinutes != value)
                {
                    workMinutes = value;
                    OnPropertyChanged(nameof(WorkMinutes));
                }
            }
        }

        public int RestMinutes
        {
            get => restMinutes;
            set
            {
                if (value < 1) throw new ArgumentException("休息时间必须大于等于1分钟");
                if (restMinutes != value)
                {
                    restMinutes = value;
                    OnPropertyChanged(nameof(RestMinutes));
                }
            }
        }
        
        private bool autoStart = false;
        private bool autoStartWork = true;

        public bool AutoStart
        {
            get => autoStart;
            set
            {
                if (autoStart != value)
                {
                    autoStart = value;
                    OnPropertyChanged(nameof(AutoStart));
                }
            }
        }

        public bool AutoStartWork
        {
            get => autoStartWork;
            set
            {
                if (autoStartWork != value)
                {
                    autoStartWork = value;
                    OnPropertyChanged(nameof(AutoStartWork));
                }
            }
        }
        
        private bool strictMode = true;
        private bool showTips = true;
        private bool playSound = true;
        private TimeSpan todayWorkDuration = TimeSpan.Zero;
        private DateTime lastWorkDate = DateTime.Today;

        public TimeSpan TodayWorkDuration
        {
            get
            {
                // 检查是否是新的一天
                if (DateTime.Today != lastWorkDate)
                {
                    lastWorkDate = DateTime.Today;
                    todayWorkDuration = TimeSpan.Zero;
                }
                return todayWorkDuration;
            }
            set
            {
                if (todayWorkDuration != value)
                {
                    todayWorkDuration = value;
                    OnPropertyChanged(nameof(TodayWorkDuration));
                }
            }
        }

        public bool StrictMode
        {
            get => strictMode;
            set
            {
                if (strictMode != value)
                {
                    strictMode = value;
                    OnPropertyChanged(nameof(StrictMode));
                }
            }
        }

        public bool ShowTips
        {
            get => showTips;
            set
            {
                if (showTips != value)
                {
                    showTips = value;
                    OnPropertyChanged(nameof(ShowTips));
                }
            }
        }

        public bool PlaySound
        {
            get => playSound;
            set
            {
                if (playSound != value)
                {
                    playSound = value;
                    OnPropertyChanged(nameof(PlaySound));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            try
            {
                Save();
                logService.LogInformation($"配置项 {propertyName} 已更新并保存");
            }
            catch (Exception ex)
            {
                logService.LogError($"保存配置项 {propertyName} 时发生错误", ex);
            }
        }

        private static string ConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "EyesGuard",
            "config.xml");

        public static Config? Load()
        {
            if (File.Exists(ConfigPath))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Config));
                    using (FileStream stream = File.OpenRead(ConfigPath))
                    {
                        var config = serializer.Deserialize(stream) as Config;
                        return config ?? new Config();
                    }
                }
                catch (Exception ex)
                {
                    logService.LogError("加载配置文件失败", ex);
                    return new Config();
                }
            }
            return new Config();
        }

        public void Save()
        {
            try
            {
                string? directoryPath = Path.GetDirectoryName(ConfigPath);
                if (directoryPath != null)
                {
                    Directory.CreateDirectory(directoryPath);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (FileStream stream = File.Create(ConfigPath))
                {
                    serializer.Serialize(stream, this);
                }
            }
            catch (Exception ex)
            {
                logService.LogError("配置保存失败", ex);
            }
        }
    }
}