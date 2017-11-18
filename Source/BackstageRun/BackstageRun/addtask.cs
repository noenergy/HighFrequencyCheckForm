using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BackstageRun
{
    public partial class addtask : Form
    {
        private DataBase DB;
        public addtask(DataBase _DB)
        {
            DB = _DB;
            InitializeComponent();
        }

        private Dictionary<string, BackstageRunConfig> taskConfig;
        private void addtask_Load(object sender, EventArgs e)
        {
            try
            {
                taskConfig = DB.GetConfig();
                foreach (string key in taskConfig.Keys)
                {
                    comboBoxBR_ID.Items.Add(key);
                }
                AllLabelTxtHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxBR_ID.Text.Equals(string.Empty))
                {
                    throw new Exception("请选择新增任务!");
                }
                Task t = new Task();

                t.BR_ID = comboBoxBR_ID.Text;
                t.DATA1 = txtData1.Text;
                t.DATA2 = txtData2.Text;
                t.DATA3 = txtData3.Text;
                t.DATA4 = txtData4.Text;
                t.DATA5 = txtData5.Text;
                t.DATA6 = txtData6.Text;
                t.DATA7 = txtData7.Text;
                if (dateTimePickerBeginDate.Enabled)
                {
                    if (dateTimePickerBeginDate.Value < DateTime.Now){throw new Exception("添加日期小于当前日期!");}
                    t.BEGINDATE = dateTimePickerBeginDate.Value.ToString();
                }

                DB.WriteTask(t);

                MessageBox.Show("新增成功!");

                txtData1.Text = "";
                txtData2.Text = "";
                txtData3.Text = "";
                txtData4.Text = "";
                txtData5.Text = "";
                txtData6.Text = "";
                txtData7.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxBR_ID_TextChanged(object sender, EventArgs e)
        {
            ShowLabelTxt(comboBoxBR_ID.Text);
        }

        private void ShowLabelTxt(string BR_ID)
        {
            try
            {
                AllLabelTxtHide();

                if (taskConfig[BR_ID].STEPTIME == 0)
                {
                    lblDateTime.Visible = true;
                    dateTimePickerBeginDate.Enabled = true;
                    dateTimePickerBeginDate.Value = DateTime.Now;
                    dateTimePickerBeginDate.Visible = true;
                }
                int paraNum = taskConfig[BR_ID].PARANUM;
                while(true)
                {
                    int num = 0;
                    if (num == paraNum) break;
                    num++;

                    lbldata1.Visible = true;
                    txtData1.Enabled = true;
                    txtData1.Visible = true;

                    if (num == paraNum) break;
                    num++;

                    lbldata2.Visible = true;
                    txtData2.Enabled = true;
                    txtData2.Visible = true;

                    if (num == paraNum) break;
                    num++;

                    lbldata3.Visible = true;
                    txtData3.Enabled = true;
                    txtData3.Visible = true;

                    if (num == paraNum) break;
                    num++;

                    lbldata4.Visible = true;
                    txtData4.Enabled = true;
                    txtData4.Visible = true;

                    if (num == paraNum) break;
                    num++;

                    lbldata5.Visible = true;
                    txtData5.Enabled = true;
                    txtData5.Visible = true;

                    if (num == paraNum) break;
                    num++;


                    lbldata6.Visible = true;
                    txtData6.Enabled = true;
                    txtData6.Visible = true;

                    if (num == paraNum) break;
                    num++;

                    lbldata7.Visible = true;
                    txtData7.Enabled = true;
                    txtData7.Visible = true;

                    break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AllLabelTxtHide()
        {
            lbldata1.Visible = false;
            lbldata2.Visible = false;
            lbldata3.Visible = false;
            lbldata4.Visible = false;
            lbldata5.Visible = false;
            lbldata6.Visible = false;
            lbldata7.Visible = false;

            txtData1.Enabled = false;
            txtData2.Enabled = false;
            txtData3.Enabled = false;
            txtData4.Enabled = false;
            txtData5.Enabled = false;
            txtData6.Enabled = false;
            txtData7.Enabled = false;

            txtData1.Visible = false;
            txtData2.Visible = false;
            txtData3.Visible = false;
            txtData4.Visible = false;
            txtData5.Visible = false;
            txtData6.Visible = false;
            txtData7.Visible = false;

            txtData1.Text = "";
            txtData2.Text = "";
            txtData3.Text = "";
            txtData4.Text = "";
            txtData5.Text = "";
            txtData6.Text = "";
            txtData7.Text = "";

            lblDateTime.Visible = false;
            dateTimePickerBeginDate.Enabled = false;
            dateTimePickerBeginDate.Visible = false;
        }
    }
}
