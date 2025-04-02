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
            lblWorkTime = new Label();
            numWorkMinutes = new NumericUpDown();
            btnStartWork = new Button();
            lblTimeRemaining = new Label();
            groupBoxSettings = new GroupBox();
            lblRestTime = new Label();
            numRestMinutes = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numWorkMinutes).BeginInit();
            groupBoxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRestMinutes).BeginInit();
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
            numWorkMinutes.Size = new Size(60, 23);
            numWorkMinutes.TabIndex = 1;
            numWorkMinutes.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // btnStartWork
            // 
            btnStartWork.Location = new Point(129, 403);
            btnStartWork.Name = "btnStartWork";
            btnStartWork.Size = new Size(100, 34);
            btnStartWork.TabIndex = 4;
            btnStartWork.Text = "开始工作";
            btnStartWork.UseVisualStyleBackColor = true;
            // 
            // lblTimeRemaining
            // 
            lblTimeRemaining.AutoSize = true;
            lblTimeRemaining.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold);
            lblTimeRemaining.Location = new Point(112, 369);
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
            numRestMinutes.Size = new Size(60, 23);
            numRestMinutes.TabIndex = 3;
            numRestMinutes.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // EyesGuardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 440);
            Controls.Add(groupBoxSettings);
            Controls.Add(lblTimeRemaining);
            Controls.Add(btnStartWork);
            Name = "EyesGuardForm";
            Text = "EyesGuard 设置";
            FormClosing += OnFormClosing;
            ((System.ComponentModel.ISupportInitialize)numWorkMinutes).EndInit();
            groupBoxSettings.ResumeLayout(false);
            groupBoxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRestMinutes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblWorkTime;
        private System.Windows.Forms.NumericUpDown numWorkMinutes;
        private System.Windows.Forms.Button btnStartWork;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Label lblRestTime;
        private System.Windows.Forms.NumericUpDown numRestMinutes;
    }
}