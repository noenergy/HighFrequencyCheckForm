namespace ViewDataAndPrint
{
    partial class ChangePWD
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtOldPWD = new System.Windows.Forms.TextBox();
            this.txtNewPWD = new System.Windows.Forms.TextBox();
            this.txtValidatePWD = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "原密码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "新密码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "确认新密码:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(201, 146);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(122, 47);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtOldPWD
            // 
            this.txtOldPWD.Location = new System.Drawing.Point(161, 16);
            this.txtOldPWD.Name = "txtOldPWD";
            this.txtOldPWD.PasswordChar = '*';
            this.txtOldPWD.Size = new System.Drawing.Size(324, 35);
            this.txtOldPWD.TabIndex = 4;
            this.txtOldPWD.UseSystemPasswordChar = true;
            // 
            // txtNewPWD
            // 
            this.txtNewPWD.Location = new System.Drawing.Point(161, 58);
            this.txtNewPWD.Name = "txtNewPWD";
            this.txtNewPWD.PasswordChar = '*';
            this.txtNewPWD.Size = new System.Drawing.Size(324, 35);
            this.txtNewPWD.TabIndex = 5;
            this.txtNewPWD.UseSystemPasswordChar = true;
            // 
            // txtValidatePWD
            // 
            this.txtValidatePWD.Location = new System.Drawing.Point(161, 100);
            this.txtValidatePWD.Name = "txtValidatePWD";
            this.txtValidatePWD.PasswordChar = '*';
            this.txtValidatePWD.Size = new System.Drawing.Size(324, 35);
            this.txtValidatePWD.TabIndex = 6;
            this.txtValidatePWD.UseSystemPasswordChar = true;
            // 
            // ChangePWD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 205);
            this.Controls.Add(this.txtValidatePWD);
            this.Controls.Add(this.txtNewPWD);
            this.Controls.Add(this.txtOldPWD);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ChangePWD";
            this.Text = "更换密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtOldPWD;
        private System.Windows.Forms.TextBox txtNewPWD;
        private System.Windows.Forms.TextBox txtValidatePWD;
    }
}