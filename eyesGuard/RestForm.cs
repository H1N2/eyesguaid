using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace eyesGuard
{
    public partial class RestForm : Form
    {
        private bool isDisposing = false;
        // 导入Windows API
        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool fBlockIt);
        private int remainingSeconds;
        private TimeSpan workDuration; // 新增：存储工作时长
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

        private readonly int restTimeSeconds;

        public RestForm(int restTimeSeconds, TimeSpan workDuration)
        {
            this.restTimeSeconds = restTimeSeconds;
            this.workDuration = workDuration;
            InitializeComponent();

            progressBar.BackColor = Color.LightGray;
            
            // 调整控件位置
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            
            // 设置进度条宽度为屏幕宽度，并放在屏幕中间位置
            progressBar.Width = screenWidth;
            progressBar.Location = new Point(0, screenHeight / 2);
            
            // 调整其他控件位置
            lblTimeRemaining.Location = new Point((screenWidth - lblTimeRemaining.Width) / 2, progressBar.Location.Y - lblTimeRemaining.Height - 20);
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
            // 设置渐变背景 (Paint事件绑定已在Designer.cs中处理)
            this.DoubleBuffered = true; // 启用双缓冲，减少闪烁

            // 设置控件样式
            lblTimeRemaining.ForeColor = ColorTranslator.FromHtml("#2E7D32"); // 深绿色文字
            lblTimeRemaining.Font = new Font("Microsoft YaHei", 16F, FontStyle.Bold);
            lblTip.ForeColor = ColorTranslator.FromHtml("#388E3C");
            lblTip.Font = new Font("Microsoft YaHei", 12F);

            // 进度条样式已在设计器中定义

            // 添加事件处理器
            restTimer.Tick += RestTimer_Tick;
            this.FormClosing += new FormClosingEventHandler(this.OnFormClosing);

            // 添加进度条动画效果
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
            
            // 初始化剩余时间
            remainingSeconds = restTimeSeconds;
            
            // 随机选择一条护眼小贴士
            Random random = new Random();
            int tipIndex = random.Next(eyeCareTips.Length);
            lblTip.Text = eyeCareTips[tipIndex];
            
            // 设置工作时长显示
            lblWorkDuration.Text = $"已工作时长: {workDuration.Hours:D2}:{workDuration.Minutes:D2}:{workDuration.Seconds:D2}";
            // 计算标签宽度并设置居中位置
            int labelWidth = TextRenderer.MeasureText(lblWorkDuration.Text, lblWorkDuration.Font).Width;
            lblWorkDuration.Location = new Point((screenWidth - labelWidth) / 2, 10);
            
            // 更新时间显示
            UpdateTimeDisplay();
            
            // 启动计时器
            restTimer.Start();
        }
        
        protected void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value > remainingSeconds)
            {
                progressBar.Value = Math.Max(remainingSeconds, progressBar.Value - 1);
            }
        }

        protected void RestTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            UpdateTimeDisplay();
            
            if (remainingSeconds <= 0)
            {
                try
                {
                    // 停止所有计时器
                    if (restTimer != null)
                    {
                        restTimer.Stop();
                    }
                    if (animationTimer != null)
                    {
                        animationTimer.Stop();
                    }

                    // 解锁输入
                    try
                    {
                        BlockInput(false);
                    }
                    catch
                    {
                        // 忽略解锁失败的错误
                    }
                }
                finally
                {
                    // 关闭窗口
                    this.Close();
                }

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
        
        protected void RestForm_SizeChanged(object sender, EventArgs e)
        {
            // 窗口大小改变时重新调整控件位置
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            // 设置进度条宽度为屏幕宽度，并保持在屏幕底部
            progressBar.Width = screenWidth;
            progressBar.Location = new Point(0, screenHeight / 2);  // 放在屏幕中间

            // 调整其他控件位置
            lblTimeRemaining.Location = new Point((screenWidth - lblTimeRemaining.Width) / 2, progressBar.Location.Y - lblTimeRemaining.Height - 20);
            lblTip.Location = new Point((screenWidth - lblTip.Width) / 2, progressBar.Bottom + 30);
            
            // 重新计算工作时长标签的位置
            int labelWidth = TextRenderer.MeasureText(lblWorkDuration.Text, lblWorkDuration.Font).Width;
            lblWorkDuration.Location = new Point((screenWidth - labelWidth) / 2, 10);

            // 强制重绘窗体
            this.Invalidate();
        }

        protected void RestForm_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                ColorTranslator.FromHtml("#E8F5E9"),  // 浅护眼绿
                ColorTranslator.FromHtml("#C7EDCC"),  // 深护眼绿
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        protected void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isDisposing && remainingSeconds > 0 && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                // 标记正在关闭
                if (animationTimer != null)
                {
                    animationTimer.Stop();
                }

                // 确保解除输入锁定
                try
                {
                    BlockInput(false);
                }
                catch
                {
                    // 忽略解锁失败的错误
                }
            }
            finally
            {
                // 不需要调用 base.OnFormClosing，因为这是一个事件处理器而不是重写
            }
        }

        // 注意：Dispose 方法已经在 Designer.cs 中定义，这里不再重复定义
    }
}