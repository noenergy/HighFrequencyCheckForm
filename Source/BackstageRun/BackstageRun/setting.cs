using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BackstageRun
{
    public partial class setting : Form
    {
        private DataBase DB;
        private Dictionary<string, BackstageRunConfig> taskConfig;
        public setting(DataBase _DB)
        {
            DB = _DB;
            InitializeComponent();
        }

        private void setting_Load(object sender, EventArgs e)
        {
            panelTask.Visible = false;
            panelConfig.Visible = false;

            taskConfig = DB.GetConfig();
            TreeLoad();
        }

        private void TreeLoad()
        {
            treeViewConfig.Nodes.Clear();
            treeViewConfig.Nodes.Add("任务列表");
            TreeNode node = new TreeNode("任务配置");

            foreach (string key in taskConfig.Keys)
            {
                node.Nodes.Add(key);
            }
            node.Nodes.Add("新增");

            treeViewConfig.Nodes.Add(node);
        }

        private void treeViewConfig_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (treeViewConfig.SelectedNode.Text.Equals("任务列表"))
                {
                    ShowTaskList();
                    panelTask.Visible = true;
                    panelTask.Show();
                    panelConfig.Visible = false;
                    panelConfig.Hide();
                }
                else if (taskConfig.Keys.Contains(treeViewConfig.SelectedNode.Text))
                {
                    panelTask.Visible = false;
                    panelTask.Hide();
                    panelConfig.Visible = true;
                    panelConfig.Show();
                    isAdd = false;
                    ShowConfig(treeViewConfig.SelectedNode.Text);
                }
                else if (treeViewConfig.SelectedNode.Text.Equals("新增"))
                {
                    panelTask.Visible = false;
                    panelTask.Hide();
                    panelConfig.Visible = true;
                    panelConfig.Show();
                    isAdd = true;
                    ShowConfig("");
                }
                else
                {
                    //nothing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
        }

#region 任务列表相关
        private void ShowTaskList()
        {
            try
            {
                dgvTaskList.DataSource = DB.GetTask("0");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnTaskListQuery_Click(object sender, EventArgs e)
        {
            try
            {
                dgvTaskList.DataSource = DB.GetTask(comboBoxStatus.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTaskDelete_Click(object sender, EventArgs e)
        {
            try
            {
                 DialogResult dr = MessageBox.Show("确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    DB.DeleteTask(dgvTaskList.CurrentRow.Cells[0].Value.ToString());
                    dgvTaskList.Rows.Remove(dgvTaskList.CurrentRow);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
#endregion
        /// <summary>
        /// 是否添加
        /// </summary>
        private bool isAdd = false;
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSAVE_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxBR_TYPE.Text.Equals("")) throw new Exception("请选择[配置类型]!");
                if (comboBoxPARANUM.Text.Equals("")) throw new Exception("请选择[参数数量]!");
                if (txtDLLNAME.Text.Trim().Equals("")) throw new Exception("请输入[类文件名]!");
                if (!txtDLLNAME.Text.ToUpper().Contains(".DLL")) txtDLLNAME.Text += ".dll";
                if (txtNAMESPACECLASS.Text.Trim().Equals("")) throw new Exception("请输入[命名空间.类]!");
                if (txtFUNCTION.Text.Trim().Equals("")) throw new Exception("请输入[执行函数]!");
                if (txtSTEPTIME.Text.Trim().Equals("")) throw new Exception("请输入[间隔时间]!");
                //if (Convert.ToInt16(txtSTEPTIME.Text) <= 0) throw new Exception("[间隔时间]必须大于0!");
                //间隔时间为0则为定时运行

                BackstageRunConfig config = new BackstageRunConfig();
                config.BR_ID = txtBR_ID.Text;
                config.BR_DESC = txtBR_DESC.Text;
                config.BR_TYPE = comboBoxBR_TYPE.Text.Equals("方法") ? 0 : 1;
                config.PARANUM = Convert.ToInt16(comboBoxPARANUM.Text);
                config.DLLNAME = txtDLLNAME.Text;
                config.NAMESPACE_CLASS = txtNAMESPACECLASS.Text;
                config.FUNCTION_NAME = txtFUNCTION.Text;
                config.STEPTIME = Convert.ToInt16(txtSTEPTIME.Text);

                if (isAdd)
                {
                    if (taskConfig.Keys.Contains(config.BR_ID))
                        throw new Exception(string.Format("[配置代码] {0} 已存在!", config.BR_ID));
                    DB.AddConfig(config);
                    taskConfig = DB.GetConfig();
                    TreeLoad();
                }
                else
                {
                    DB.UpdateConfig(config);
                    taskConfig = DB.GetConfig();
                }

                MessageBox.Show("操作成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    panelTask.Visible = false;
                    panelConfig.Visible = false;
                    DB.DeleteConfig(txtBR_ID.Text);
                    taskConfig = DB.GetConfig();
                    TreeLoad();
                    MessageBox.Show("操作成功!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ShowConfig(string BR_ID)
        {
            try
            {
                if (isAdd)
                {
                    btnDelete.Enabled = false;
                    btnDelete.Visible = false;
                    txtBR_ID.ReadOnly = false;
                    txtBR_ID.Text = "";
                    txtBR_DESC.Text = "";
                    comboBoxBR_TYPE.Text = "";
                    comboBoxPARANUM.Text = "";
                    txtDLLNAME.Text = "";
                    txtNAMESPACECLASS.Text = "";
                    txtFUNCTION.Text = "";
                    txtSTEPTIME.Text = "";
                }
                else
                {
                    btnDelete.Enabled = true;
                    btnDelete.Visible = true;
                    txtBR_ID.ReadOnly = true;
                    txtBR_ID.Text = taskConfig[BR_ID].BR_ID;
                    txtBR_DESC.Text = taskConfig[BR_ID].BR_DESC;
                    comboBoxBR_TYPE.Text = taskConfig[BR_ID].BR_TYPE.Equals(0) ? "方法" : "窗体";
                    comboBoxPARANUM.Text = taskConfig[BR_ID].PARANUM.ToString();
                    txtDLLNAME.Text = taskConfig[BR_ID].DLLNAME;
                    txtNAMESPACECLASS.Text = taskConfig[BR_ID].NAMESPACE_CLASS;
                    txtFUNCTION.Text = taskConfig[BR_ID].FUNCTION_NAME;
                    txtSTEPTIME.Text = taskConfig[BR_ID].STEPTIME.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtSTEPTIME_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            } 
        }
    }
}
