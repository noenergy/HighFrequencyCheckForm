using System;
using System.Windows.Forms;
using DatabaseProcessing;

namespace ViewDataAndPrint
{
    public partial class ChangePWD : Form
    {
        EntityManager manager;
        public ChangePWD(EntityManager entityManager) 
        {
            manager = entityManager;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOldPWD.Text.Trim().Length <= 0) { throw new Exception("请输入原密码!"); }
                if (txtNewPWD.Text.Trim().Length <= 0) { throw new Exception("请输入新密码!"); }
                if (txtValidatePWD.Text.Trim().Length <= 0) { throw new Exception("请输入确认新密码!"); }
                if (!txtNewPWD.Text.Trim().Equals(txtValidatePWD.Text.Trim())) { throw new Exception("两次输入的密码不一致!"); }

                EntityBase pwdEntity = new EntityBase("OPERATION_ID");
                pwdEntity["TYPE"] = "PROMISE";
                if (!manager.GetEntity(ref pwdEntity))
                {
                    throw new Exception("未找到管理员密码!");
                }
                if (pwdEntity["VALUE"].ToString().Trim().Equals(txtOldPWD.Text.Trim()))
                {
                    pwdEntity["VALUE"] = txtNewPWD.Text.Trim();
                    manager.UpdateEntity(pwdEntity);
                    MessageBox.Show("密码更换成功!");
                    this.Close();
                }
                else
                {
                    throw new Exception("密码不正确!");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
