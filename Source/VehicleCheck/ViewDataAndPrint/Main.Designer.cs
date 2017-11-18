namespace ViewDataAndPrint
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.dGVChenkData = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LANE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAR_PLATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PASSTIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPEED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LENGHT_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WIDTH_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HEIGHT_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IS_PRINT = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cB_AutoRefresh = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblLane_Id = new System.Windows.Forms.Label();
            this.lblPlate = new System.Windows.Forms.Label();
            this.lblPasstime = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblLenght = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGVChenkData)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVChenkData
            // 
            this.dGVChenkData.AllowUserToAddRows = false;
            this.dGVChenkData.AllowUserToDeleteRows = false;
            this.dGVChenkData.AllowUserToOrderColumns = true;
            this.dGVChenkData.AllowUserToResizeRows = false;
            this.dGVChenkData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGVChenkData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVChenkData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dGVChenkData.BackgroundColor = System.Drawing.Color.White;
            this.dGVChenkData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVChenkData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.LANE_ID,
            this.CAR_PLATE,
            this.PASSTIME,
            this.SPEED,
            this.LENGHT_DESC,
            this.WIDTH_DESC,
            this.HEIGHT_DESC,
            this.IS_PRINT});
            this.dGVChenkData.Location = new System.Drawing.Point(0, 0);
            this.dGVChenkData.Margin = new System.Windows.Forms.Padding(4);
            this.dGVChenkData.MultiSelect = false;
            this.dGVChenkData.Name = "dGVChenkData";
            this.dGVChenkData.ReadOnly = true;
            this.dGVChenkData.RowHeadersWidth = 18;
            this.dGVChenkData.RowTemplate.Height = 37;
            this.dGVChenkData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVChenkData.Size = new System.Drawing.Size(1082, 742);
            this.dGVChenkData.TabIndex = 0;
            this.dGVChenkData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVChenkData_CellClick);
            this.dGVChenkData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVChenkData_CellDoubleClick);
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ID.DataPropertyName = "ID";
            this.ID.FillWeight = 11F;
            this.ID.HeaderText = "记录号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // LANE_ID
            // 
            this.LANE_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LANE_ID.DataPropertyName = "LANE_ID";
            this.LANE_ID.FillWeight = 11F;
            this.LANE_ID.HeaderText = "车道";
            this.LANE_ID.Name = "LANE_ID";
            this.LANE_ID.ReadOnly = true;
            // 
            // CAR_PLATE
            // 
            this.CAR_PLATE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CAR_PLATE.DataPropertyName = "CAR_PLATE";
            this.CAR_PLATE.FillWeight = 11F;
            this.CAR_PLATE.HeaderText = "车牌";
            this.CAR_PLATE.Name = "CAR_PLATE";
            this.CAR_PLATE.ReadOnly = true;
            // 
            // PASSTIME
            // 
            this.PASSTIME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PASSTIME.DataPropertyName = "PASSTIME";
            this.PASSTIME.FillWeight = 11F;
            this.PASSTIME.HeaderText = "通过时间";
            this.PASSTIME.Name = "PASSTIME";
            this.PASSTIME.ReadOnly = true;
            // 
            // SPEED
            // 
            this.SPEED.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SPEED.DataPropertyName = "SPEED";
            this.SPEED.FillWeight = 11F;
            this.SPEED.HeaderText = "车速";
            this.SPEED.Name = "SPEED";
            this.SPEED.ReadOnly = true;
            // 
            // LENGHT_DESC
            // 
            this.LENGHT_DESC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LENGHT_DESC.DataPropertyName = "LENGHT_DESC";
            this.LENGHT_DESC.FillWeight = 11F;
            this.LENGHT_DESC.HeaderText = "车长";
            this.LENGHT_DESC.Name = "LENGHT_DESC";
            this.LENGHT_DESC.ReadOnly = true;
            // 
            // WIDTH_DESC
            // 
            this.WIDTH_DESC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WIDTH_DESC.DataPropertyName = "WIDTH_DESC";
            this.WIDTH_DESC.FillWeight = 11F;
            this.WIDTH_DESC.HeaderText = "车宽";
            this.WIDTH_DESC.Name = "WIDTH_DESC";
            this.WIDTH_DESC.ReadOnly = true;
            // 
            // HEIGHT_DESC
            // 
            this.HEIGHT_DESC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HEIGHT_DESC.DataPropertyName = "HEIGHT_DESC";
            this.HEIGHT_DESC.FillWeight = 11F;
            this.HEIGHT_DESC.HeaderText = "车高";
            this.HEIGHT_DESC.Name = "HEIGHT_DESC";
            this.HEIGHT_DESC.ReadOnly = true;
            // 
            // IS_PRINT
            // 
            this.IS_PRINT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IS_PRINT.DataPropertyName = "IS_PRINT";
            this.IS_PRINT.FillWeight = 11F;
            this.IS_PRINT.HeaderText = "是否已打印";
            this.IS_PRINT.Name = "IS_PRINT";
            this.IS_PRINT.ReadOnly = true;
            // 
            // cB_AutoRefresh
            // 
            this.cB_AutoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cB_AutoRefresh.AutoSize = true;
            this.cB_AutoRefresh.Location = new System.Drawing.Point(1112, 692);
            this.cB_AutoRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.cB_AutoRefresh.Name = "cB_AutoRefresh";
            this.cB_AutoRefresh.Size = new System.Drawing.Size(138, 28);
            this.cB_AutoRefresh.TabIndex = 1;
            this.cB_AutoRefresh.Text = "自动刷新";
            this.cB_AutoRefresh.UseVisualStyleBackColor = true;
            this.cB_AutoRefresh.CheckedChanged += new System.EventHandler(this.cB_AutoRefresh_CheckedChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1112, 620);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(138, 48);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 1000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1112, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "车辆记录号:";
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblID.ForeColor = System.Drawing.Color.Red;
            this.lblID.Location = new System.Drawing.Point(1112, 44);
            this.lblID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(136, 24);
            this.lblID.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1112, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "车道号:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1112, 122);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "车牌:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1112, 176);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 24);
            this.label5.TabIndex = 8;
            this.label5.Text = "通过时间:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1112, 254);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 24);
            this.label6.TabIndex = 9;
            this.label6.Text = "车速:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1112, 306);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 24);
            this.label7.TabIndex = 10;
            this.label7.Text = "长:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1112, 356);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 24);
            this.label8.TabIndex = 11;
            this.label8.Text = "宽:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1112, 408);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 24);
            this.label9.TabIndex = 12;
            this.label9.Text = "高:";
            // 
            // lblLane_Id
            // 
            this.lblLane_Id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLane_Id.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLane_Id.ForeColor = System.Drawing.Color.Red;
            this.lblLane_Id.Location = new System.Drawing.Point(1112, 96);
            this.lblLane_Id.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblLane_Id.Name = "lblLane_Id";
            this.lblLane_Id.Size = new System.Drawing.Size(136, 24);
            this.lblLane_Id.TabIndex = 13;
            // 
            // lblPlate
            // 
            this.lblPlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPlate.ForeColor = System.Drawing.Color.Red;
            this.lblPlate.Location = new System.Drawing.Point(1112, 150);
            this.lblPlate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPlate.Name = "lblPlate";
            this.lblPlate.Size = new System.Drawing.Size(136, 24);
            this.lblPlate.TabIndex = 14;
            // 
            // lblPasstime
            // 
            this.lblPasstime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPasstime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPasstime.ForeColor = System.Drawing.Color.Red;
            this.lblPasstime.Location = new System.Drawing.Point(1112, 202);
            this.lblPasstime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPasstime.Name = "lblPasstime";
            this.lblPasstime.Size = new System.Drawing.Size(136, 48);
            this.lblPasstime.TabIndex = 15;
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpeed.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSpeed.ForeColor = System.Drawing.Color.Red;
            this.lblSpeed.Location = new System.Drawing.Point(1112, 280);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(136, 24);
            this.lblSpeed.TabIndex = 16;
            // 
            // lblLenght
            // 
            this.lblLenght.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLenght.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLenght.ForeColor = System.Drawing.Color.Red;
            this.lblLenght.Location = new System.Drawing.Point(1112, 332);
            this.lblLenght.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblLenght.Name = "lblLenght";
            this.lblLenght.Size = new System.Drawing.Size(136, 24);
            this.lblLenght.TabIndex = 17;
            // 
            // lblWidth
            // 
            this.lblWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWidth.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWidth.ForeColor = System.Drawing.Color.Red;
            this.lblWidth.Location = new System.Drawing.Point(1112, 382);
            this.lblWidth.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(136, 24);
            this.lblWidth.TabIndex = 18;
            // 
            // lblHeight
            // 
            this.lblHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHeight.ForeColor = System.Drawing.Color.Red;
            this.lblHeight.Location = new System.Drawing.Point(1112, 434);
            this.lblHeight.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(136, 24);
            this.lblHeight.TabIndex = 19;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 742);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.lblLenght);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblPasstime);
            this.Controls.Add(this.lblPlate);
            this.Controls.Add(this.lblLane_Id);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cB_AutoRefresh);
            this.Controls.Add(this.dGVChenkData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "[S101省道K115+250 超限车辆行驶状态 - 上海至杭州方向高速检测长宽高实时记录]";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVChenkData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cB_AutoRefresh;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dGVChenkData;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblLane_Id;
        private System.Windows.Forms.Label lblPlate;
        private System.Windows.Forms.Label lblPasstime;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblLenght;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LANE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAR_PLATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PASSTIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPEED;
        private System.Windows.Forms.DataGridViewTextBoxColumn LENGHT_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn WIDTH_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn HEIGHT_DESC;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IS_PRINT;
    }
}

