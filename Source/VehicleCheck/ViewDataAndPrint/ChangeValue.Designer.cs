namespace ViewDataAndPrint
{
    partial class ChangeValue
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
            this.lblType = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 20);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(58, 24);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "修改";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(16, 66);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(270, 35);
            this.txtValue.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(96, 122);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(111, 39);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "确定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ChangeValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 183);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ChangeValue";
            this.Text = "ChangeValue";
            this.Load += new System.EventHandler(this.ChangeValue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnSubmit;
    }
}