using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer; // 明确指定使用Windows.Forms的Timer

namespace eyesGuard
{
    public partial class RestForm : Form
    {
        // 导入Windows API
        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool fBlockIt);
        private Timer restTimer;
        private int remainingSeconds;
        private TimeSpan workDuration; // 新增：存储工作时长
        private ProgressBar progressBar;
        private string[] eyeCareTips = new string[]
        {
            "请将视线移向远处，看看窗外的风景",
            "轻轻闭上眼睛，让眼睛得到充分休息",
            "做眼保健操，缓解眼部疲劳",
            "每工作一小时，至少休息5-10分钟",
            "保持正确坐姿，屏幕与眼睛保持合适距离",
            "眨眼频率应保持在每分钟15-20次",
            "使用电脑时，每隔20分钟向远处看20秒",
            "保持充足的睡眠，有助于缓解眼部疲劳",
            "多喝水，保持身体和眼睛的水分充足",
            "定期进行眼部检查，预防眼部疾病"
        };
        
        public RestForm(int restTimeSeconds, TimeSpan workDuration)
        {
            InitializeComponent();
            
            this.workDuration = workDuration; // 保存工作时长
            this.remainingSeconds = restTimeSeconds;
            
            // 设置窗体属性
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.ShowInTaskbar = false; // 在任务栏中隐藏
            this.Cursor = Cursors.No; // 显示禁止光标

            // 初始化进度条
            progressBar.Maximum = restTimeSeconds;
            progressBar.Value = restTimeSeconds;
            progressBar.ForeColor = Color.Green;
            progressBar.BackColor = Color.LightGray;
            
            // 调整控件位置
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            
            lblTimeRemaining.Location = new Point((screenWidth - lblTimeRemaining.Width) / 2, screenHeight / 3);
            progressBar.Location = new Point((screenWidth - progressBar.Width) / 2, lblTimeRemaining.Bottom + 20);
            lblTip.Location = new Point((screenWidth - lblTip.Width) / 2, progressBar.Bottom + 30);
            
            // 锁定输入
            try
            {
                if (this.IsHandleCreated)
                {
                    BlockInput(true); // 阻止所有键盘和鼠标输入
                }
            }
            catch (Exception ex)
            {
                // 记录错误但继续执行
                Console.WriteLine($"无法锁定输入: {ex.Message}");
            }
            // 设置渐变背景
            this.Paint += (sender, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    this.ClientRectangle,
                    ColorTranslator.FromHtml("#E8F5E9"),  // 浅护眼绿
                    ColorTranslator.FromHtml("#C7EDCC"),  // 深护眼绿
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            };
            this.DoubleBuffered = true; // 启用双缓冲，减少闪烁

            // 设置控件样式
            lblTimeRemaining.ForeColor = ColorTranslator.FromHtml("#2E7D32"); // 深绿色文字
            lblTimeRemaining.Font = new Font("Microsoft YaHei", 16F, FontStyle.Bold);
            lblTip.ForeColor = ColorTranslator.FromHtml("#388E3C");
            lblTip.Font = new Font("Microsoft YaHei", 12F);

            // 自定义进度条样式
            progressBar = new ProgressBar();
            progressBar.Size = new Size(400, 30);
            progressBar.Style = ProgressBarStyle.Continuous;
            this.Controls.Add(progressBar);

            // 添加进度条动画效果
            Timer animationTimer = new Timer();
            animationTimer.Interval = 50; // 50毫秒更新一次
            animationTimer.Tick += (sender, e) =>
            {
                if (progressBar.Value > remainingSeconds)
                {
                    progressBar.Value = Math.Max(remainingSeconds, progressBar.Value - 1);
                }
            };
            animationTimer.Start();
            
            // 初始化计时器
            restTimer = new Timer();
            restTimer.Interval = 1000; // 1秒
            restTimer.Tick += RestTimer_Tick;
            
            // 初始化剩余时间
            remainingSeconds = restTimeSeconds;
            
            // 随机选择一条护眼小贴士
            Random random = new Random();
            int tipIndex = random.Next(eyeCareTips.Length);
            lblTip.Text = eyeCareTips[tipIndex];
            
            // 添加工作时长显示
            Label lblWorkDuration = new Label();
            lblWorkDuration.AutoSize = true;
            lblWorkDuration.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblWorkDuration.Text = $"已工作时长: {workDuration.Hours:D2}:{workDuration.Minutes:D2}:{workDuration.Seconds:D2}";
            // 计算标签宽度并设置居中位置
            int labelWidth = TextRenderer.MeasureText(lblWorkDuration.Text, lblWorkDuration.Font).Width;
            lblWorkDuration.Location = new Point((screenWidth - labelWidth) / 2, 10);
            this.Controls.Add(lblWorkDuration);
            
            // 更新时间显示
            UpdateTimeDisplay();
            
            // 启动计时器
            restTimer.Start();
        }
        
        private void RestTimer_Tick(object? sender, EventArgs e)
        {
            remainingSeconds--;
            UpdateTimeDisplay();
            
            if (remainingSeconds <= 0)
            {
                restTimer.Stop();
                
                // 解锁输入
                try
                {
                    BlockInput(false);
                }
                catch
                {
                    // 忽略解锁失败的错误
                }
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        
        private void UpdateTimeDisplay()
        {
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            lblTimeRemaining.Text = $"休息时间剩余: {minutes:00}:{seconds:00}";

            // 更新进度条
            progressBar.Value = remainingSeconds;
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // 禁用所有键盘输入
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            // 禁用键盘释放
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // 禁用鼠标点击
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            // 禁用鼠标释放
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // 禁用鼠标移动
            base.OnMouseMove(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // 防止Alt+F4关闭窗口
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x200; // CS_NOCLOSE
                return cp;
            }
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 只允许通过计时器结束来关闭窗口
            if (remainingSeconds > 0 && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
            else
            {
                base.OnFormClosing(e);
            }
        }
    }
}