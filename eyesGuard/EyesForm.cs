using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using eyesGuard.Extensions;

namespace eyesGuard
{
    public partial class EyesGuardForm : Form
    {

        // 默认工作时间25分钟（1500秒）
        private Config config = Config.Load() ?? new Config();
        private int remainingSeconds;
        private DateTime workStartTime; // 新增：记录工作开始时间

        public EyesGuardForm()
        {
            InitializeComponent();

            // 注册窗体事件
            this.Resize += EyesGuardForm_Resize;
            this.FormClosing += EyesGuardForm_FormClosing;
            InitializeTrayIcon();
            InitializeTimer();
            LoadConfig();

            // 初始化复选框事件
            InitializeCheckBoxEvents();
            btnStartWork.Paint += (s, e) =>
            {
                var btn = (Button)s;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddRoundedRectangle(btn.ClientRectangle, 5);
                    btn.Region = new Region(path);
                }
            };


            // 初始化UI控件事件
            btnStartWork.Click += BtnStartWork_Click;
            numWorkMinutes.Value = config.WorkMinutes;
            numRestMinutes.Value = config.RestMinutes;

            // 自启动选项已在 InitializeTrayIcon() 方法中添加

            // 添加数值改变事件
            numWorkMinutes.ValueChanged += NumMinutes_ValueChanged;
            numRestMinutes.ValueChanged += NumMinutes_ValueChanged;

            // 初始化剩余时间
            remainingSeconds = config.WorkMinutes * 60;
            UpdateTrayText();

            // 初始化今日工作时长显示
            lblTodayWorkDuration.Text = "今日工作时长：00:00:00";
        }

        private void LoadConfig()
        {
            config = Config.Load();
            if (config == null)
            {
                config = new Config();
                config.Save();
            }

            // 应用加载的配置
            numWorkMinutes.Value = config.WorkMinutes;
            numRestMinutes.Value = config.RestMinutes;
            chkAutoStartWork.Checked = config.AutoStartWork;
            chkStrictMode.Checked = config.StrictMode;
            chkShowTips.Checked = config.ShowTips;
            chkPlaySound.Checked = config.PlaySound;
        }

        // 设置选项的CheckedChanged事件处理
        private void InitializeCheckBoxEvents()
        {
            chkAutoStartWork.CheckedChanged += NewConfigOption_CheckedChanged;
            chkStrictMode.CheckedChanged += NewConfigOption_CheckedChanged;
            chkShowTips.CheckedChanged += NewConfigOption_CheckedChanged;
            chkPlaySound.CheckedChanged += NewConfigOption_CheckedChanged;
        }

        private void NewConfigOption_CheckedChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void InitializeTimer()
        {
            workTimer.Interval = 1000; // 1秒
            workTimer.Tick += WorkTimer_Tick;
        }

        private void InitializeTrayIcon()
        {
            // 设置托盘图标
            try
            {
                // 尝试从资源加载图标
                string iconPath = "eye_ico.ico";
                if (File.Exists(iconPath))
                {
                    trayIcon.Icon = new Icon(iconPath);
                }
                else
                {
                    // 如果找不到图标文件，使用应用程序默认图标
                    trayIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                }
            }
            catch (Exception)
            {
                // 如果加载图标失败，使用应用程序默认图标
                trayIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            }

            // 初始化托盘菜单
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
            trayIcon.DoubleClick += TrayIcon_DoubleClick;

            // 先清空菜单项
            trayMenu.Items.Clear();

            // 添加基本菜单项
            trayMenu.Items.Add("设置", null, OnSettings);
            trayMenu.Items.Add("立即休息", null, OnRestNow);
            trayMenu.Items.Add(new ToolStripSeparator());
            trayMenu.Items.Add("退出", null, OnExit);

            // 添加自启动选项到托盘菜单
            ToolStripMenuItem autoStartItem = new ToolStripMenuItem("开机自启动");
            autoStartItem.CheckOnClick = true;
            autoStartItem.Checked = config.AutoStart;
            autoStartItem.Click += AutoStartItem_Click;

            // 在第二个位置插入（索引为1）
            trayMenu.Items.Insert(1, autoStartItem);
            trayMenu.Items.Insert(2, new ToolStripSeparator());
        }

        private void WorkTimer_Tick(object? sender, EventArgs e)
        {
            remainingSeconds--;
            UpdateTodayWorkDuration();
            UpdateTrayText();

            if (remainingSeconds <= 120 && remainingSeconds % 2 == 0)
            {
                // 闪烁托盘图标
                trayIcon.Icon = (trayIcon.Icon == SystemIcons.Application) ? SystemIcons.Exclamation : SystemIcons.Application;
            }

            if (remainingSeconds <= 0)
            {
                workTimer.Stop();
                StartRest();
            }
        }

        private void UpdateTrayText()
        {
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;

            // 计算当前工作持续时间
            TimeSpan currentWorkDuration = DateTime.Now - workStartTime;
            int workMinutes = (int)currentWorkDuration.TotalMinutes;

            // 格式化今日工作时长
            TimeSpan todayDuration = config.TodayWorkDuration;
            string todayWorkHours = $"{todayDuration.TotalHours:F1}";

            // 更新托盘提示文本
            trayIcon.Text = $"你已经持续工作了{workMinutes}分钟，\n还有{minutes:00}分钟{seconds:00}秒电脑就会被锁定，\n今天已经持续工作{todayWorkHours}小时";
            lblTimeRemaining.Text = $"剩余时间：{minutes:00}:{seconds:00}";
        }

        private void UpdateTodayWorkDuration()
        {
            if (lblTodayWorkDuration != null)
            {
                var duration = config.TodayWorkDuration;
                lblTodayWorkDuration.Text = $"今日工作时长：{duration.TotalHours:F1}小时";
            }
        }

        private void BtnStartWork_Click(object? sender, EventArgs e)
        {
            remainingSeconds = (int)numWorkMinutes.Value * 60;
            workStartTime = DateTime.Now; // 记录工作开始时间
            workTimer.Start();
            this.Hide();
            UpdateTrayText();
            trayIcon.ShowBalloonTip(3000, "EyesGuard", "工作计时开始！", ToolTipIcon.Info);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (config.AutoStartWork)
            {
                BtnStartWork_Click(null, EventArgs.Empty);
            }
        }

        private void StartRest()
        {
            workTimer.Stop();
            int restTimeSeconds = (int)numRestMinutes.Value * 60;
            TimeSpan workDuration = DateTime.Now - workStartTime; // 计算工作时长
            config.TodayWorkDuration += workDuration; // 累加今日工作时长
            config.Save(); // 保存配置
            using (RestForm restForm = new RestForm(restTimeSeconds, workDuration))
            {
                this.Hide();
                restForm.ShowDialog();
            }
        }

        private void TrayIcon_DoubleClick(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void OnSettings(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void OnRestNow(object? sender, EventArgs e)
        {
            StartRest();
        }

        private void OnExit(object? sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出 EyesGuard 吗？", "确认退出",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void CheckAutoStartSetting()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    if (key != null)
                    {
                        if (key.GetValue("EyesGuard") != null)
                        {
                            // 已设置自启动
                            config.AutoStart = true;
                        }
                        else
                        {
                            // 未设置自启动
                            config.AutoStart = false;
                        }
                    }
                }
            }
            catch
            {
                // 访问注册表失败
                config.AutoStart = false;
            }
        }

        private void SetAutoStart(bool autoStart)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    if (key != null)
                    {
                        if (autoStart)
                        {
                            string appPath = Application.ExecutablePath;
                            key.SetValue("EyesGuard", appPath);
                        }
                        else
                        {
                            key.DeleteValue("EyesGuard", false);
                        }

                        config.AutoStart = autoStart;
                        config.Save();
                    }
                }
            }
            catch
            {
                MessageBox.Show("设置开机自启动失败，请以管理员身份运行程序。", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveConfig()
        {
            config.WorkMinutes = (int)numWorkMinutes.Value;
            config.RestMinutes = (int)numRestMinutes.Value;
            config.AutoStartWork = chkAutoStartWork.Checked;
            config.StrictMode = chkStrictMode.Checked;
            config.ShowTips = chkShowTips.Checked;
            config.PlaySound = chkPlaySound.Checked;
            config.Save();
        }

        private void NumMinutes_ValueChanged(object? sender, EventArgs e)
        {
            SaveConfig();
        }

        private void AutoStartItem_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                SetAutoStart(item.Checked);
            }
        }

        private void EyesGuardForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                trayIcon.ShowBalloonTip(3000, "EyesGuard", "程序已最小化到系统托盘", ToolTipIcon.Info);
            }
        }

        private void EyesGuardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;  // 取消关闭操作
                this.Hide();      // 隐藏窗体
                trayIcon.ShowBalloonTip(3000, "EyesGuard", "程序已最小化到系统托盘", ToolTipIcon.Info);
            }
            else
            {
                // 如果是应用程序退出，保存配置并清理资源
                config.Save();
                if (trayIcon != null)
                {
                    trayIcon.Visible = false;
                    trayIcon.Dispose();
                }
            }
        }
    }
}