using Timer = System.Windows.Forms.Timer;

namespace eyesGuard
{
    partial class RestForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && !isDisposing)
                {
                    isDisposing = true;

                    // 先停止所有计时器
                    if (restTimer != null)
                    {
                        restTimer.Stop();
                    }
                    if (animationTimer != null)
                    {
                        animationTimer.Stop();
                    }

                    // 解除事件绑定
                    this.FormClosing -= OnFormClosing;
                    this.Paint -= RestForm_Paint;
                    this.SizeChanged -= RestForm_SizeChanged;
                    this.Resize -= RestForm_SizeChanged;
                    if (restTimer != null)
                    {
                        restTimer.Tick -= RestTimer_Tick;
                    }

                    // 释放计时器资源
                    if (restTimer != null)
                    {
                        restTimer.Dispose();
                        restTimer = null;
                    }
                    if (animationTimer != null)
                    {
                        animationTimer.Dispose();
                        animationTimer = null;
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

                    // 释放其他托管资源
                    if (components != null)
                    {
                        components.Dispose();
                        components = null;
                    }

                    // 释放字体资源
                    if (lblTimeRemaining != null && lblTimeRemaining.Font != null)
                    {
                        lblTimeRemaining.Font.Dispose();
                    }
                    if (lblTip != null && lblTip.Font != null)
                    {
                        lblTip.Font.Dispose();
                    }
                    if (lblWorkDuration != null && lblWorkDuration.Font != null)
                    {
                        lblWorkDuration.Font.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTimeRemaining = new Label();
            lblTip = new Label();
            progressBar = new ProgressBar();
            lblWorkDuration = new Label();
            restTimer = new Timer(components);
            animationTimer = new Timer(components);
            SuspendLayout();
            // 
            // lblTimeRemaining
            // 
            lblTimeRemaining.Anchor = AnchorStyles.None;
            lblTimeRemaining.AutoSize = true;
            lblTimeRemaining.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            lblTimeRemaining.ForeColor = Color.FromArgb(46, 125, 50);
            lblTimeRemaining.Location = new Point(300, 227);
            lblTimeRemaining.Name = "lblTimeRemaining";
            lblTimeRemaining.Size = new Size(220, 30);
            lblTimeRemaining.TabIndex = 0;
            lblTimeRemaining.Text = "休息时间剩余: 05:00";
            lblTimeRemaining.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTip
            // 
            lblTip.Anchor = AnchorStyles.None;
            lblTip.AutoSize = true;
            lblTip.Font = new Font("Microsoft YaHei UI", 12F);
            lblTip.ForeColor = Color.FromArgb(56, 142, 60);
            lblTip.Location = new Point(300, 340);
            lblTip.Name = "lblTip";
            lblTip.Size = new Size(90, 21);
            lblTip.TabIndex = 1;
            lblTip.Text = "护眼小贴士";
            lblTip.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            progressBar.BackColor = Color.LightGray;
            progressBar.ForeColor = Color.Green;
            progressBar.Location = new Point(12, 283);
            progressBar.Maximum = 300;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(800, 34);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 2;
            progressBar.Value = 300;
            // 
            // lblWorkDuration
            // 
            lblWorkDuration.AutoSize = true;
            lblWorkDuration.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            lblWorkDuration.ForeColor = Color.FromArgb(46, 125, 50);
            lblWorkDuration.Location = new Point(300, 185);
            lblWorkDuration.Name = "lblWorkDuration";
            lblWorkDuration.Size = new Size(170, 22);
            lblWorkDuration.TabIndex = 3;
            lblWorkDuration.Text = "已工作时长: 00:00:00";
            lblWorkDuration.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // restTimer
            // 
            restTimer.Interval = 1000;
            // 
            // animationTimer
            // 
            animationTimer.Interval = 50;
            // 
            // RestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(232, 245, 233);
            ClientSize = new Size(800, 680);
            Controls.Add(lblWorkDuration);
            Controls.Add(progressBar);
            Controls.Add(lblTimeRemaining);
            Controls.Add(lblTip);
            Cursor = Cursors.No;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "RestForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EyesGuard - 休息时间";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            FormClosing += OnFormClosing;
            Paint += RestForm_Paint;
            ResumeLayout(false);
            PerformLayout();
        }

        // 注意: 所有事件处理方法已在 RestForm.cs 中定义

        #endregion

        private Label lblTimeRemaining;
        private Label lblTip;
        private ProgressBar progressBar;
        private Label lblWorkDuration;
        private System.Windows.Forms.Timer restTimer;
        private System.Windows.Forms.Timer animationTimer;
    }
}