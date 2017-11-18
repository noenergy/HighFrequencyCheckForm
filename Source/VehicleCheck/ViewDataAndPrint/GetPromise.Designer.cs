namespace ViewDataAndPrint
{
    partial class GetPromise
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入管理员权限密码:";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(35, 90);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(258, 35);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(35, 148);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(103, 43);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(190, 148);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(103, 43);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "更换";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // GetPromise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 219);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GetPromise";
            this.Text = "权限管理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnChange;
    }
}