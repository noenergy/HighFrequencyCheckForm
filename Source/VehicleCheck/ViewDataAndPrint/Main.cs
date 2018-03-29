using System;
using System.Data;
using System.Windows.Forms;
using DatabaseProcessing;
using System.Xml;

namespace ViewDataAndPrint
{
    public partial class Main : Form
    {
        EntityManager manager;
        EntityBase carResult;
        WhereObjectList where;
        string printer = string.Empty;
        string imgServer = string.Empty;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                timerRefresh.Interval = 300000; //设置自动刷新时间 ms
                dGVChenkData.AutoGenerateColumns = false;
                manager = new EntityManager();
                carResult = new EntityBase("CAR_RESULT");
                where = new WhereObjectList();
                RefreshData();
                ReadPrintConfig();

                //读取地址转换信息
                EntityBase systemLinkEntity = new EntityBase("SYSTEM_LINK");
                systemLinkEntity["SYSTEM_ID"] = "IMG_TO";
                if (!manager.GetEntity(ref systemLinkEntity)) { throw new Exception("图片地址转换信息丢失!"); }
                imgServer = systemLinkEntity["SYSTEM_NAME"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                where.Clear();
                where.add("ID", WhereObjectType.GreaterThan, 0);
                where.add("LENGHT", WhereObjectType.GreaterThan, 600); //只显示5米2以上车
                where.add("LENGHT", WhereObjectType.LessThan, 1800); //只显示18米以下车
                where.add("SPEED", WhereObjectType.LessThanOrEqualTo, 80);//只显示80km/h以下
                where.add("SPEED", WhereObjectType.GreaterThan, 0);
                where.add("WIDTH", WhereObjectType.GreaterThan, 160);//只显示宽度1米6以上车
                where.add("CAR_PLATE", WhereObjectType.NotEqualTo, "无车牌");
                where.add("CAR_PLATE", WhereObjectType.NotEqualTo, "无匹配");
                DataTable table = manager.GetTableEx(carResult, where, 1000, "PASSTIME DESC");
                if (table.Rows.Count <= 0) { throw new Exception("无车辆数据!"); }
                table.Columns.Add("LENGHT_DESC", typeof(string));
                table.Columns.Add("WIDTH_DESC", typeof(string));
                table.Columns.Add("HEIGHT_DESC", typeof(string));
                foreach (DataRow dr in table.Rows)
                {
                    dr["LENGHT_DESC"] = Math.Round(double.Parse(dr["LENGHT"].ToString()) / 100, 3).ToString("0.000");
                    //dr["WIDTH_DESC"] = Math.Round(double.Parse(dr["WIDTH"].ToString()) / 100, 3).ToString("0.000");
                    if (double.Parse(dr["WIDTH"].ToString()) < 2.5)
                    {
                        dr["WIDTH_DESC"] = "小于2.5米";
                    }
                    else
                    {
                        dr["WIDTH_DESC"] = Math.Round(double.Parse(dr["WIDTH"].ToString()) / 100, 3).ToString("0.000");
                    }
                    if (double.Parse(dr["HEIGHT"].ToString()) < 4)
                    {
                        dr["HEIGHT_DESC"] = "小于4米";
                    }
                    else
                    {
                        dr["HEIGHT_DESC"] = Math.Round(double.Parse(dr["HEIGHT"].ToString()) / 100, 3).ToString("0.000");
                    }
                }
                dGVChenkData.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dGVChenkData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    PrePrint pp = new PrePrint(dGVChenkData.Rows[e.RowIndex].Cells[0].Value.ToString(), printer, imgServer, this);
                    pp.Show();
                }
                else
                {
                    MessageBox.Show("当前无可选数据!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void cB_AutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (cB_AutoRefresh.Checked)
            {
                timerRefresh.Start();
            }
            else
            {
                timerRefresh.Stop();
            }
        }

        private void dGVChenkData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    lblID.Text = dGVChenkData.Rows[e.RowIndex].Cells[0].Value.ToString();
                    lblLane_Id.Text = dGVChenkData.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblPlate.Text = dGVChenkData.Rows[e.RowIndex].Cells[2].Value.ToString();
                    lblPasstime.Text = dGVChenkData.Rows[e.RowIndex].Cells[3].Value.ToString();
                    lblSpeed.Text = dGVChenkData.Rows[e.RowIndex].Cells[4].Value.ToString();
                    lblLenght.Text = dGVChenkData.Rows[e.RowIndex].Cells[5].Value.ToString();
                    lblWidth.Text = dGVChenkData.Rows[e.RowIndex].Cells[6].Value.ToString();
                    lblHeight.Text = dGVChenkData.Rows[e.RowIndex].Cells[7].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadPrintConfig()
        {

            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            XmlDocument doc = new XmlDocument();
            doc.Load(path + "\\printer.config");

            XmlNodeList Node = doc.SelectSingleNode("config").ChildNodes;

            for (int n = 0; n < Node.Count; n++)
            {
                XmlNodeList cNode = Node.Item(n).ChildNodes;
                int nodeNum = cNode.Count;

                if (Node.Item(n).Name.Equals("printer"))
                {
                    try
                    {
                        printer = Node[n].InnerXml;
                    }
                    catch
                    {
                        throw new Exception("打印机配置错误！");
                    }
                }
            }
            if (string.IsNullOrEmpty(printer)) { throw new Exception("配置信息不完整!"); }
        }
    }
}
