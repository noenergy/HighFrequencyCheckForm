namespace BackstageRun
{
    partial class setting
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
            this.treeViewConfig = new System.Windows.Forms.TreeView();
            this.panelConfig = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.comboBoxBR_TYPE = new System.Windows.Forms.ComboBox();
            this.comboBoxPARANUM = new System.Windows.Forms.ComboBox();
            this.lblSecond = new System.Windows.Forms.Label();
            this.btnSAVE = new System.Windows.Forms.Button();
            this.txtSTEPTIME = new System.Windows.Forms.TextBox();
            this.txtFUNCTION = new System.Windows.Forms.TextBox();
            this.txtNAMESPACECLASS = new System.Windows.Forms.TextBox();
            this.txtDLLNAME = new System.Windows.Forms.TextBox();
            this.txtBR_DESC = new System.Windows.Forms.TextBox();
            this.txtBR_ID = new System.Windows.Forms.TextBox();
            this.lblSTEPTIME = new System.Windows.Forms.Label();
            this.lblFUNCTION = new System.Windows.Forms.Label();
            this.lblNAMESPACECLASS = new System.Windows.Forms.Label();
            this.lblDLLNAME = new System.Windows.Forms.Label();
            this.lblPARANUM = new System.Windows.Forms.Label();
            this.lblBR_TYPE = new System.Windows.Forms.Label();
            this.lblBR_DESC = new System.Windows.Forms.Label();
            this.lblBR_ID = new System.Windows.Forms.Label();
            this.panelTask = new System.Windows.Forms.Panel();
            this.btnTaskDelete = new System.Windows.Forms.Button();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.btnTaskListQuery = new System.Windows.Forms.Button();
            this.dgvTaskList = new System.Windows.Forms.DataGridView();
            this.panelConfig.SuspendLayout();
            this.panelTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewConfig
            // 
            this.treeViewConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewConfig.Location = new System.Drawing.Point(1, 1);
            this.treeViewConfig.Name = "treeViewConfig";
            this.treeViewConfig.Size = new System.Drawing.Size(103, 259);
            this.treeViewConfig.TabIndex = 0;
            this.treeViewConfig.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewConfig_NodeMouseDoubleClick);
            // 
            // panelConfig
            // 
            this.panelConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelConfig.Controls.Add(this.btnDelete);
            this.panelConfig.Controls.Add(this.comboBoxBR_TYPE);
            this.panelConfig.Controls.Add(this.comboBoxPARANUM);
            this.panelConfig.Controls.Add(this.lblSecond);
            this.panelConfig.Controls.Add(this.btnSAVE);
            this.panelConfig.Controls.Add(this.txtSTEPTIME);
            this.panelConfig.Controls.Add(this.txtFUNCTION);
            this.panelConfig.Controls.Add(this.txtNAMESPACECLASS);
            this.panelConfig.Controls.Add(this.txtDLLNAME);
            this.panelConfig.Controls.Add(this.txtBR_DESC);
            this.panelConfig.Controls.Add(this.txtBR_ID);
            this.panelConfig.Controls.Add(this.lblSTEPTIME);
            this.panelConfig.Controls.Add(this.lblFUNCTION);
            this.panelConfig.Controls.Add(this.lblNAMESPACECLASS);
            this.panelConfig.Controls.Add(this.lblDLLNAME);
            this.panelConfig.Controls.Add(this.lblPARANUM);
            this.panelConfig.Controls.Add(this.lblBR_TYPE);
            this.panelConfig.Controls.Add(this.lblBR_DESC);
            this.panelConfig.Controls.Add(this.lblBR_ID);
            this.panelConfig.Location = new System.Drawing.Point(106, 1);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(348, 259);
            this.panelConfig.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(178, 229);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // comboBoxBR_TYPE
            // 
            this.comboBoxBR_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBR_TYPE.FormattingEnabled = true;
            this.comboBoxBR_TYPE.Items.AddRange(new object[] {
            "方法",
            "窗体"});
            this.comboBoxBR_TYPE.Location = new System.Drawing.Point(70, 79);
            this.comboBoxBR_TYPE.Name = "comboBoxBR_TYPE";
            this.comboBoxBR_TYPE.Size = new System.Drawing.Size(263, 20);
            this.comboBoxBR_TYPE.TabIndex = 3;
            // 
            // comboBoxPARANUM
            // 
            this.comboBoxPARANUM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPARANUM.FormattingEnabled = true;
            this.comboBoxPARANUM.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboBoxPARANUM.Location = new System.Drawing.Point(70, 103);
            this.comboBoxPARANUM.Name = "comboBoxPARANUM";
            this.comboBoxPARANUM.Size = new System.Drawing.Size(263, 20);
            this.comboBoxPARANUM.TabIndex = 2;
            // 
            // lblSecond
            // 
            this.lblSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSecond.AutoSize = true;
            this.lblSecond.Location = new System.Drawing.Point(198, 202);
            this.lblSecond.Name = "lblSecond";
            this.lblSecond.Size = new System.Drawing.Size(131, 12);
            this.lblSecond.TabIndex = 1;
            this.lblSecond.Text = "秒  (若是定时运行填0)";
            // 
            // btnSAVE
            // 
            this.btnSAVE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSAVE.Location = new System.Drawing.Point(259, 229);
            this.btnSAVE.Name = "btnSAVE";
            this.btnSAVE.Size = new System.Drawing.Size(75, 23);
            this.btnSAVE.TabIndex = 0;
            this.btnSAVE.Text = "SAVE";
            this.btnSAVE.UseVisualStyleBackColor = true;
            this.btnSAVE.Click += new System.EventHandler(this.btnSAVE_Click);
            // 
            // txtSTEPTIME
            // 
            this.txtSTEPTIME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTEPTIME.Location = new System.Drawing.Point(70, 199);
            this.txtSTEPTIME.Name = "txtSTEPTIME";
            this.txtSTEPTIME.Size = new System.Drawing.Size(123, 21);
            this.txtSTEPTIME.TabIndex = 0;
            this.txtSTEPTIME.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSTEPTIME_KeyPress);
            // 
            // txtFUNCTION
            // 
            this.txtFUNCTION.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFUNCTION.Location = new System.Drawing.Point(70, 175);
            this.txtFUNCTION.Name = "txtFUNCTION";
            this.txtFUNCTION.Size = new System.Drawing.Size(263, 21);
            this.txtFUNCTION.TabIndex = 0;
            // 
            // txtNAMESPACECLASS
            // 
            this.txtNAMESPACECLASS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNAMESPACECLASS.Location = new System.Drawing.Point(88, 151);
            this.txtNAMESPACECLASS.Name = "txtNAMESPACECLASS";
            this.txtNAMESPACECLASS.Size = new System.Drawing.Size(245, 21);
            this.txtNAMESPACECLASS.TabIndex = 0;
            // 
            // txtDLLNAME
            // 
            this.txtDLLNAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDLLNAME.Location = new System.Drawing.Point(70, 127);
            this.txtDLLNAME.Name = "txtDLLNAME";
            this.txtDLLNAME.Size = new System.Drawing.Size(264, 21);
            this.txtDLLNAME.TabIndex = 0;
            // 
            // txtBR_DESC
            // 
            this.txtBR_DESC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBR_DESC.Location = new System.Drawing.Point(70, 41);
            this.txtBR_DESC.Multiline = true;
            this.txtBR_DESC.Name = "txtBR_DESC";
            this.txtBR_DESC.Size = new System.Drawing.Size(264, 35);
            this.txtBR_DESC.TabIndex = 0;
            // 
            // txtBR_ID
            // 
            this.txtBR_ID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBR_ID.Location = new System.Drawing.Point(69, 17);
            this.txtBR_ID.Name = "txtBR_ID";
            this.txtBR_ID.Size = new System.Drawing.Size(264, 21);
            this.txtBR_ID.TabIndex = 0;
            // 
            // lblSTEPTIME
            // 
            this.lblSTEPTIME.AutoSize = true;
            this.lblSTEPTIME.Location = new System.Drawing.Point(5, 202);
            this.lblSTEPTIME.Name = "lblSTEPTIME";
            this.lblSTEPTIME.Size = new System.Drawing.Size(59, 12);
            this.lblSTEPTIME.TabIndex = 0;
            this.lblSTEPTIME.Text = "间隔时间:";
            // 
            // lblFUNCTION
            // 
            this.lblFUNCTION.AutoSize = true;
            this.lblFUNCTION.Location = new System.Drawing.Point(5, 178);
            this.lblFUNCTION.Name = "lblFUNCTION";
            this.lblFUNCTION.Size = new System.Drawing.Size(59, 12);
            this.lblFUNCTION.TabIndex = 0;
            this.lblFUNCTION.Text = "执行函数:";
            // 
            // lblNAMESPACECLASS
            // 
            this.lblNAMESPACECLASS.AutoSize = true;
            this.lblNAMESPACECLASS.Location = new System.Drawing.Point(5, 154);
            this.lblNAMESPACECLASS.Name = "lblNAMESPACECLASS";
            this.lblNAMESPACECLASS.Size = new System.Drawing.Size(77, 12);
            this.lblNAMESPACECLASS.TabIndex = 0;
            this.lblNAMESPACECLASS.Text = "命名空间.类:";
            // 
            // lblDLLNAME
            // 
            this.lblDLLNAME.AutoSize = true;
            this.lblDLLNAME.Location = new System.Drawing.Point(5, 130);
            this.lblDLLNAME.Name = "lblDLLNAME";
            this.lblDLLNAME.Size = new System.Drawing.Size(59, 12);
            this.lblDLLNAME.TabIndex = 0;
            this.lblDLLNAME.Text = "类文件名:";
            // 
            // lblPARANUM
            // 
            this.lblPARANUM.AutoSize = true;
            this.lblPARANUM.Location = new System.Drawing.Point(5, 106);
            this.lblPARANUM.Name = "lblPARANUM";
            this.lblPARANUM.Size = new System.Drawing.Size(59, 12);
            this.lblPARANUM.TabIndex = 0;
            this.lblPARANUM.Text = "参数数量:";
            // 
            // lblBR_TYPE
            // 
            this.lblBR_TYPE.AutoSize = true;
            this.lblBR_TYPE.Location = new System.Drawing.Point(5, 82);
            this.lblBR_TYPE.Name = "lblBR_TYPE";
            this.lblBR_TYPE.Size = new System.Drawing.Size(59, 12);
            this.lblBR_TYPE.TabIndex = 0;
            this.lblBR_TYPE.Text = "配置类型:";
            // 
            // lblBR_DESC
            // 
            this.lblBR_DESC.AutoSize = true;
            this.lblBR_DESC.Location = new System.Drawing.Point(5, 44);
            this.lblBR_DESC.Name = "lblBR_DESC";
            this.lblBR_DESC.Size = new System.Drawing.Size(59, 12);
            this.lblBR_DESC.TabIndex = 0;
            this.lblBR_DESC.Text = "配置描述:";
            // 
            // lblBR_ID
            // 
            this.lblBR_ID.AutoSize = true;
            this.lblBR_ID.Location = new System.Drawing.Point(5, 20);
            this.lblBR_ID.Name = "lblBR_ID";
            this.lblBR_ID.Size = new System.Drawing.Size(59, 12);
            this.lblBR_ID.TabIndex = 0;
            this.lblBR_ID.Text = "配置代码:";
            // 
            // panelTask
            // 
            this.panelTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTask.Controls.Add(this.btnTaskDelete);
            this.panelTask.Controls.Add(this.comboBoxStatus);
            this.panelTask.Controls.Add(this.btnTaskListQuery);
            this.panelTask.Controls.Add(this.dgvTaskList);
            this.panelTask.Location = new System.Drawing.Point(109, 1);
            this.panelTask.Name = "panelTask";
            this.panelTask.Size = new System.Drawing.Size(348, 259);
            this.panelTask.TabIndex = 0;
            // 
            // btnTaskDelete
            // 
            this.btnTaskDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaskDelete.Location = new System.Drawing.Point(132, 220);
            this.btnTaskDelete.Name = "btnTaskDelete";
            this.btnTaskDelete.Size = new System.Drawing.Size(75, 23);
            this.btnTaskDelete.TabIndex = 0;
            this.btnTaskDelete.Text = "Delete";
            this.btnTaskDelete.UseVisualStyleBackColor = true;
            this.btnTaskDelete.Click += new System.EventHandler(this.btnTaskDelete_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "-1",
            "0",
            "1",
            "2"});
            this.comboBoxStatus.Location = new System.Drawing.Point(213, 222);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(51, 20);
            this.comboBoxStatus.TabIndex = 0;
            this.comboBoxStatus.Text = "0";
            // 
            // btnTaskListQuery
            // 
            this.btnTaskListQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaskListQuery.Location = new System.Drawing.Point(270, 220);
            this.btnTaskListQuery.Name = "btnTaskListQuery";
            this.btnTaskListQuery.Size = new System.Drawing.Size(75, 23);
            this.btnTaskListQuery.TabIndex = 0;
            this.btnTaskListQuery.Text = "Query";
            this.btnTaskListQuery.UseVisualStyleBackColor = true;
            this.btnTaskListQuery.Click += new System.EventHandler(this.btnTaskListQuery_Click);
            // 
            // dgvTaskList
            // 
            this.dgvTaskList.AllowUserToAddRows = false;
            this.dgvTaskList.AllowUserToDeleteRows = false;
            this.dgvTaskList.AllowUserToResizeColumns = false;
            this.dgvTaskList.AllowUserToResizeRows = false;
            this.dgvTaskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTaskList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTaskList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskList.Location = new System.Drawing.Point(3, 3);
            this.dgvTaskList.MultiSelect = false;
            this.dgvTaskList.Name = "dgvTaskList";
            this.dgvTaskList.ReadOnly = true;
            this.dgvTaskList.RowHeadersWidth = 20;
            this.dgvTaskList.RowTemplate.Height = 23;
            this.dgvTaskList.Size = new System.Drawing.Size(342, 212);
            this.dgvTaskList.TabIndex = 0;
            // 
            // setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 261);
            this.Controls.Add(this.treeViewConfig);
            this.Controls.Add(this.panelConfig);
            this.Controls.Add(this.panelTask);
            this.Name = "setting";
            this.Text = "BackstageRun Manager";
            this.Load += new System.EventHandler(this.setting_Load);
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            this.panelTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewConfig;
        private System.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.Panel panelTask;
        private System.Windows.Forms.Label lblBR_ID;
        private System.Windows.Forms.DataGridView dgvTaskList;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Button btnTaskListQuery;
        private System.Windows.Forms.Button btnTaskDelete;
        private System.Windows.Forms.Label lblSTEPTIME;
        private System.Windows.Forms.Label lblFUNCTION;
        private System.Windows.Forms.Label lblNAMESPACECLASS;
        private System.Windows.Forms.Label lblDLLNAME;
        private System.Windows.Forms.Label lblPARANUM;
        private System.Windows.Forms.Label lblBR_TYPE;
        private System.Windows.Forms.Label lblBR_DESC;
        private System.Windows.Forms.TextBox txtSTEPTIME;
        private System.Windows.Forms.TextBox txtFUNCTION;
        private System.Windows.Forms.TextBox txtNAMESPACECLASS;
        private System.Windows.Forms.TextBox txtDLLNAME;
        private System.Windows.Forms.TextBox txtBR_DESC;
        private System.Windows.Forms.TextBox txtBR_ID;
        private System.Windows.Forms.Button btnSAVE;
        private System.Windows.Forms.Label lblSecond;
        private System.Windows.Forms.ComboBox comboBoxPARANUM;
        private System.Windows.Forms.ComboBox comboBoxBR_TYPE;
        private System.Windows.Forms.Button btnDelete;

    }
}