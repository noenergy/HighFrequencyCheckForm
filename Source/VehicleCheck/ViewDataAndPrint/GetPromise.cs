using System;
using System.Windows.Forms;
using DatabaseProcessing;

namespace ViewDataAndPrint
{
    public partial class GetPromise : Form
    {
        private PrePrint pp;
        private EntityManager manager;
        public GetPromise(PrePrint _PrePrint, EntityManager _entityManager)
        {
            pp = _PrePrint;
            manager = _entityManager;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                EntityBase pwdEntity = new EntityBase("OPERATION_ID");
                pwdEntity["TYPE"] = "PROMISE";
                if (!manager.GetEntity(ref pwdEntity))
                {
                    throw new Exception("未找到管理员密码!");
                }
                if (pwdEntity["VALUE"].ToString().Trim().Equals(txtPwd.Text.Trim()))
                {
                    pp.SetPromise();
                    MessageBox.Show("授权完成!");
                    this.Close();
                }
                else
                {
                    throw new Exception("密码不正确!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePWD cp = new ChangePWD(manager);
                cp.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
