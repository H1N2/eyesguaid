using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Win32;
using eyesGuard.Extensions;

namespace eyesGuard
{
    public partial class EyesGuardForm : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private System.Windows.Forms.Timer workTimer;
        private CheckBox chkAutoStartWork;
        private CheckBox chkStrictMode;
        private CheckBox chkShowTips;
        private CheckBox chkPlaySound;
        private Label lblTodayWorkDuration;

        // 默认工作时间25分钟（1500秒）
        private Config config = Config.Load() ?? new Config();
        private int remainingSeconds;
        private DateTime workStartTime; // 新增：记录工作开始时间

        public EyesGuardForm()
        {
            InitializeComponent();
            InitializeTrayIcon();
            InitializeTimer();
            InitializeNewConfigOptions();
            LoadConfig();

            // 设置窗体属性和样式
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#F5F5F5");
            this.Font = new Font("Microsoft YaHei", 9F);

            // 美化按钮样式
            btnStartWork.FlatStyle = FlatStyle.Flat;
            btnStartWork.BackColor = ColorTranslator.FromHtml("#4CAF50");
            btnStartWork.ForeColor = Color.White;
            btnStartWork.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold);
            btnStartWork.Size = new Size(120, 35);
            btnStartWork.Cursor = Cursors.Hand;
            btnStartWork.Paint += (s, e) => {
                var btn = (Button)s;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddRoundedRectangle(btn.ClientRectangle, 5);
                    btn.Region = new Region(path);
                }
            };

            // 美化数值输入控件
            foreach (NumericUpDown num in new[] { numWorkMinutes, numRestMinutes })
            {
                num.BorderStyle = BorderStyle.FixedSingle;
                num.Font = new Font("Microsoft YaHei", 10F);
                num.Size = new Size(80, 25);
            }

            // 初始化UI控件事件
            btnStartWork.Click += BtnStartWork_Click;
            numWorkMinutes.Value = config.WorkMinutes;
            numRestMinutes.Value = config.RestMinutes;

            // 添加自启动选项到托盘菜单
            ToolStripMenuItem autoStartItem = new ToolStripMenuItem("开机自启动");
            autoStartItem.CheckOnClick = true;
            autoStartItem.Checked = config.AutoStart;
            autoStartItem.Click += AutoStartItem_Click;
            trayMenu.Items.Insert(1, autoStartItem);
            trayMenu.Items.Insert(2, new ToolStripSeparator());

            // 添加数值改变事件
            numWorkMinutes.ValueChanged += NumMinutes_ValueChanged;
            numRestMinutes.ValueChanged += NumMinutes_ValueChanged;

            // 初始化剩余时间
            remainingSeconds = config.WorkMinutes * 60;
            UpdateTrayText();

            // 初始化今日工作时长显示
            lblTodayWorkDuration = new Label();
            lblTodayWorkDuration.AutoSize = true;
            lblTodayWorkDuration.Font = new Font("Microsoft YaHei", 12F);
            lblTodayWorkDuration.ForeColor = ColorTranslator.FromHtml("#424242");
            lblTodayWorkDuration.Location = new Point(120, 340);
            lblTodayWorkDuration.Text = "今日工作时长：00:00:00";
            this.Controls.Add(lblTodayWorkDuration);
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

        private void InitializeNewConfigOptions()
        {
            // 创建选项分组面板
            GroupBox optionsGroup = new GroupBox();
            optionsGroup.Text = "设置选项";
            optionsGroup.Font = new Font("Microsoft YaHei", 9F, FontStyle.Bold);
            optionsGroup.Size = new Size(360, 180);
            optionsGroup.Location = new Point(20, 150);
            optionsGroup.BackColor = Color.White;
            this.Controls.Add(optionsGroup);

            // 自动开始工作
            chkAutoStartWork = CreateStyledCheckBox("启动程序后自动开始计时", new Point(15, 30));
            optionsGroup.Controls.Add(chkAutoStartWork);

            // 严格模式
            chkStrictMode = CreateStyledCheckBox("严格模式（锁定输入设备）", new Point(15, 60));
            optionsGroup.Controls.Add(chkStrictMode);

            // 显示护眼小贴士
            chkShowTips = CreateStyledCheckBox("显示护眼小贴士", new Point(15, 90));
            optionsGroup.Controls.Add(chkShowTips);

            // 休息提醒声音
            chkPlaySound = CreateStyledCheckBox("休息提醒声音", new Point(15, 120));
            optionsGroup.Controls.Add(chkPlaySound);
        }

        private CheckBox CreateStyledCheckBox(string text, Point location)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Text = text;
            checkBox.AutoSize = true;
            checkBox.Location = location;
            checkBox.Font = new Font("Microsoft YaHei", 9F);
            checkBox.ForeColor = ColorTranslator.FromHtml("#424242");
            checkBox.CheckedChanged += NewConfigOption_CheckedChanged;
            return checkBox;
        }

        private void NewConfigOption_CheckedChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void InitializeTimer()
        {
            workTimer = new System.Windows.Forms.Timer();
            workTimer.Interval = 1000; // 1秒
            workTimer.Tick += WorkTimer_Tick;
        }

        private void InitializeTrayIcon()
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("设置", null, OnSettings);
            trayMenu.Items.Add("马上休息", null, OnRestNow);
            trayMenu.Items.Add("-"); // 分隔线
            trayMenu.Items.Add("退出", null, OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "EyesGuard - 准备开始";
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
            trayIcon.DoubleClick += TrayIcon_DoubleClick;
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
                if (restForm.ShowDialog() == DialogResult.OK)
                {
                    // 休息结束，重新开始工作
                    remainingSeconds = (int)numWorkMinutes.Value * 60;
                    workStartTime = DateTime.Now; // 重新记录工作开始时间
                    workTimer.Start();
                    UpdateTrayText();
                }
            }
            this.Show();
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

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                trayIcon.Visible = false;
            }
        }
    }
}