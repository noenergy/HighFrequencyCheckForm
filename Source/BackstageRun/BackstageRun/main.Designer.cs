namespace BackstageRun
{
    partial class main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.notifyIconButton = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip4NotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.contextMenuStrip4NotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(524, 58);
            this.btnStart.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(150, 46);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(524, 116);
            this.btnStop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(150, 46);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.Gainsboro;
            this.btnMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMin.Location = new System.Drawing.Point(610, 4);
            this.btnMin.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(32, 32);
            this.btnMin.TabIndex = 0;
            this.btnMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.DarkGray;
            this.BtnClose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnClose.Location = new System.Drawing.Point(648, 4);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(32, 32);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(688, 44);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "BackstageRun";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblName_MouseDown);
            this.lblName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblName_MouseMove);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.GrayText;
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(8, 58);
            this.txtLog.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(500, 160);
            this.txtLog.TabIndex = 0;
            // 
            // notifyIconButton
            // 
            this.notifyIconButton.ContextMenuStrip = this.contextMenuStrip4NotifyIcon;
            this.notifyIconButton.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconButton.Icon")));
            this.notifyIconButton.Text = "BackstageRun";
            this.notifyIconButton.Visible = true;
            this.notifyIconButton.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconButton_MouseDoubleClick);
            // 
            // contextMenuStrip4NotifyIcon
            // 
            this.contextMenuStrip4NotifyIcon.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip4NotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmStart,
            this.tsmStop,
            this.tsmExit});
            this.contextMenuStrip4NotifyIcon.Name = "contextMenuStrip4NotifyIcon";
            this.contextMenuStrip4NotifyIcon.ShowImageMargin = false;
            this.contextMenuStrip4NotifyIcon.Size = new System.Drawing.Size(118, 112);
            // 
            // tsmStart
            // 
            this.tsmStart.BackColor = System.Drawing.SystemColors.Control;
            this.tsmStart.Name = "tsmStart";
            this.tsmStart.Size = new System.Drawing.Size(117, 36);
            this.tsmStart.Text = "Start";
            this.tsmStart.Click += new System.EventHandler(this.tsmStart_Click);
            // 
            // tsmStop
            // 
            this.tsmStop.BackColor = System.Drawing.SystemColors.Control;
            this.tsmStop.Name = "tsmStop";
            this.tsmStop.Size = new System.Drawing.Size(117, 36);
            this.tsmStop.Text = "Stop";
            this.tsmStop.Click += new System.EventHandler(this.tsmStop_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.BackColor = System.Drawing.SystemColors.Control;
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(117, 36);
            this.tsmExit.Text = "Exit";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(600, 174);
            this.btnSetting.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(74, 46);
            this.btnSetting.TabIndex = 0;
            this.btnSetting.Text = "SET";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(524, 174);
            this.btnAddTask.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(74, 46);
            this.btnAddTask.TabIndex = 1;
            this.btnAddTask.Text = "TASK";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 232);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "main";
            this.Text = "BackstageRun";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.main_Load);
            this.contextMenuStrip4NotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label btnMin;
        private System.Windows.Forms.Label BtnClose;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.NotifyIcon notifyIconButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4NotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem tsmStart;
        private System.Windows.Forms.ToolStripMenuItem tsmStop;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnAddTask;
    }
}

