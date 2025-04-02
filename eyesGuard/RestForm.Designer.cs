namespace eyesGuard
{
    partial class RestForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            
            // lblTimeRemaining
            this.lblTimeRemaining.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTimeRemaining.AutoSize = true;
            this.lblTimeRemaining.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTimeRemaining.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTimeRemaining.Location = new System.Drawing.Point(0, 0);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(350, 40);
            this.lblTimeRemaining.TabIndex = 0;
            this.lblTimeRemaining.Text = "休息时间剩余: 05:00";
            this.lblTimeRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // progressBar
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar.Location = new System.Drawing.Point(0, 50);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(400, 30);
            this.progressBar.TabIndex = 2;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            
            // lblTip
            this.lblTip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTip.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTip.Location = new System.Drawing.Point(0, 0);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(500, 30);
            this.lblTip.TabIndex = 1;
            this.lblTip.Text = "护眼小贴士";
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // RestForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lblTimeRemaining);
            this.Controls.Add(this.lblTip);
            this.Name = "RestForm";
            this.Text = "EyesGuard - 休息时间";
            this.Load += new System.EventHandler(this.RestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void RestForm_Load(object sender, System.EventArgs e)
        {
            // 在窗体加载时居中显示标签
            CenterLabels();
        }

        private void CenterLabels()
        {
            // 居中显示时间标签
            lblTimeRemaining.Left = (this.ClientSize.Width - lblTimeRemaining.Width) / 2;
            lblTimeRemaining.Top = this.ClientSize.Height / 3;

            // 居中显示提示标签
            lblTip.Left = (this.ClientSize.Width - lblTip.Width) / 2;
            lblTip.Top = this.ClientSize.Height / 2;
        }

        #endregion

        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Label lblTip;
    }
}