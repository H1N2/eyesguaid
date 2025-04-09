namespace eyesGuard
{
    partial class EyesGuardForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblWorkTime = new Label();
            numWorkMinutes = new NumericUpDown();
            btnStartWork = new Button();
            lblTimeRemaining = new Label();
            groupBoxSettings = new GroupBox();
            lblRestTime = new Label();
            numRestMinutes = new NumericUpDown();
            optionsGroup = new GroupBox();
            chkAutoStartWork = new CheckBox();
            chkStrictMode = new CheckBox();
            chkShowTips = new CheckBox();
            chkPlaySound = new CheckBox();
            lblTodayWorkDuration = new Label();
            trayMenu = new ContextMenuStrip(components);
            trayIcon = new NotifyIcon(components);
            workTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)numWorkMinutes).BeginInit();
            groupBoxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRestMinutes).BeginInit();
            optionsGroup.SuspendLayout();
            SuspendLayout();
            // 
            // lblWorkTime
            // 
            lblWorkTime.AutoSize = true;
            lblWorkTime.Location = new Point(20, 34);
            lblWorkTime.Name = "lblWorkTime";
            lblWorkTime.Size = new Size(91, 17);
            lblWorkTime.TabIndex = 0;
            lblWorkTime.Text = "工作时间(分钟):";
            // 
            // numWorkMinutes
            // 
            numWorkMinutes.Location = new Point(130, 32);
            numWorkMinutes.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            numWorkMinutes.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numWorkMinutes.Name = "numWorkMinutes";
            numWorkMinutes.Size = new Size(80, 23);
            numWorkMinutes.TabIndex = 1;
            numWorkMinutes.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // btnStartWork
            // 
            btnStartWork.BackColor = Color.FromArgb(76, 175, 80);
            btnStartWork.Cursor = Cursors.Hand;
            btnStartWork.FlatStyle = FlatStyle.Flat;
            btnStartWork.Font = new Font("微软雅黑", 10F, FontStyle.Bold);
            btnStartWork.ForeColor = Color.White;
            btnStartWork.Location = new Point(122, 409);
            btnStartWork.Name = "btnStartWork";
            btnStartWork.Size = new Size(129, 35);
            btnStartWork.TabIndex = 4;
            btnStartWork.Text = "开始工作";
            btnStartWork.UseVisualStyleBackColor = false;
            // 
            // lblTimeRemaining
            // 
            lblTimeRemaining.AutoSize = true;
            lblTimeRemaining.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            lblTimeRemaining.Location = new Point(122, 374);
            lblTimeRemaining.Name = "lblTimeRemaining";
            lblTimeRemaining.Size = new Size(129, 22);
            lblTimeRemaining.TabIndex = 5;
            lblTimeRemaining.Text = "剩余时间: 00:00";
            lblTimeRemaining.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBoxSettings
            // 
            groupBoxSettings.Controls.Add(lblWorkTime);
            groupBoxSettings.Controls.Add(numWorkMinutes);
            groupBoxSettings.Controls.Add(lblRestTime);
            groupBoxSettings.Controls.Add(numRestMinutes);
            groupBoxSettings.Location = new Point(20, 23);
            groupBoxSettings.Name = "groupBoxSettings";
            groupBoxSettings.Size = new Size(360, 125);
            groupBoxSettings.TabIndex = 6;
            groupBoxSettings.TabStop = false;
            groupBoxSettings.Text = "时间设置";
            // 
            // lblRestTime
            // 
            lblRestTime.AutoSize = true;
            lblRestTime.Location = new Point(20, 74);
            lblRestTime.Name = "lblRestTime";
            lblRestTime.Size = new Size(91, 17);
            lblRestTime.TabIndex = 2;
            lblRestTime.Text = "休息时间(分钟):";
            // 
            // numRestMinutes
            // 
            numRestMinutes.Location = new Point(130, 71);
            numRestMinutes.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numRestMinutes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numRestMinutes.Name = "numRestMinutes";
            numRestMinutes.Size = new Size(80, 23);
            numRestMinutes.TabIndex = 3;
            numRestMinutes.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // optionsGroup
            // 
            optionsGroup.BackColor = Color.White;
            optionsGroup.Controls.Add(chkAutoStartWork);
            optionsGroup.Controls.Add(chkStrictMode);
            optionsGroup.Controls.Add(chkShowTips);
            optionsGroup.Controls.Add(chkPlaySound);
            optionsGroup.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            optionsGroup.Location = new Point(20, 150);
            optionsGroup.Name = "optionsGroup";
            optionsGroup.Size = new Size(360, 180);
            optionsGroup.TabIndex = 7;
            optionsGroup.TabStop = false;
            optionsGroup.Text = "设置选项";
            // 
            // chkAutoStartWork
            // 
            chkAutoStartWork.AutoSize = true;
            chkAutoStartWork.Font = new Font("微软雅黑", 9F);
            chkAutoStartWork.ForeColor = Color.FromArgb(66, 66, 66);
            chkAutoStartWork.Location = new Point(15, 30);
            chkAutoStartWork.Name = "chkAutoStartWork";
            chkAutoStartWork.Size = new Size(159, 21);
            chkAutoStartWork.TabIndex = 0;
            chkAutoStartWork.Text = "启动程序后自动开始计时";
            chkAutoStartWork.UseVisualStyleBackColor = true;
            // 
            // chkStrictMode
            // 
            chkStrictMode.AutoSize = true;
            chkStrictMode.Font = new Font("微软雅黑", 9F);
            chkStrictMode.ForeColor = Color.FromArgb(66, 66, 66);
            chkStrictMode.Location = new Point(15, 60);
            chkStrictMode.Name = "chkStrictMode";
            chkStrictMode.Size = new Size(171, 21);
            chkStrictMode.TabIndex = 1;
            chkStrictMode.Text = "严格模式（锁定输入设备）";
            chkStrictMode.UseVisualStyleBackColor = true;
            // 
            // chkShowTips
            // 
            chkShowTips.AutoSize = true;
            chkShowTips.Font = new Font("微软雅黑", 9F);
            chkShowTips.ForeColor = Color.FromArgb(66, 66, 66);
            chkShowTips.Location = new Point(15, 90);
            chkShowTips.Name = "chkShowTips";
            chkShowTips.Size = new Size(111, 21);
            chkShowTips.TabIndex = 2;
            chkShowTips.Text = "显示护眼小贴士";
            chkShowTips.UseVisualStyleBackColor = true;
            // 
            // chkPlaySound
            // 
            chkPlaySound.AutoSize = true;
            chkPlaySound.Font = new Font("微软雅黑", 9F);
            chkPlaySound.ForeColor = Color.FromArgb(66, 66, 66);
            chkPlaySound.Location = new Point(15, 120);
            chkPlaySound.Name = "chkPlaySound";
            chkPlaySound.Size = new Size(99, 21);
            chkPlaySound.TabIndex = 3;
            chkPlaySound.Text = "休息提醒声音";
            chkPlaySound.UseVisualStyleBackColor = true;
            // 
            // lblTodayWorkDuration
            // 
            lblTodayWorkDuration.AutoSize = true;
            lblTodayWorkDuration.Font = new Font("微软雅黑", 12F);
            lblTodayWorkDuration.ForeColor = Color.FromArgb(66, 66, 66);
            lblTodayWorkDuration.Location = new Point(120, 340);
            lblTodayWorkDuration.Name = "lblTodayWorkDuration";
            lblTodayWorkDuration.Size = new Size(184, 21);
            lblTodayWorkDuration.TabIndex = 8;
            lblTodayWorkDuration.Text = "今日工作时长：00:00:00";
            // 
            // trayMenu
            // 
            trayMenu.Name = "trayMenu";
            trayMenu.Size = new Size(61, 4);
            // 
            // trayIcon
            // 
            trayIcon.Text = "EyesGuard - 准备开始";
            trayIcon.Visible = true;
            // 
            // workTimer
            // 
            workTimer.Interval = 1000;
            // 
            // EyesGuardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(410, 456);
            Controls.Add(lblTodayWorkDuration);
            Controls.Add(optionsGroup);
            Controls.Add(groupBoxSettings);
            Controls.Add(lblTimeRemaining);
            Controls.Add(btnStartWork);
            Font = new Font("微软雅黑", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "EyesGuardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EyesGuard 设置";
            FormClosing += EyesGuardForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)numWorkMinutes).EndInit();
            groupBoxSettings.ResumeLayout(false);
            groupBoxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRestMinutes).EndInit();
            optionsGroup.ResumeLayout(false);
            optionsGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblWorkTime;
        private NumericUpDown numWorkMinutes;
        private Button btnStartWork;
        private Label lblTimeRemaining;
        private GroupBox groupBoxSettings;
        private Label lblRestTime;
        private NumericUpDown numRestMinutes;
        private GroupBox optionsGroup;
        private CheckBox chkAutoStartWork;
        private CheckBox chkStrictMode;
        private CheckBox chkShowTips;
        private CheckBox chkPlaySound;
        private Label lblTodayWorkDuration;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private System.Windows.Forms.Timer workTimer;
    }
}