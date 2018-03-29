using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DatabaseProcessing;

namespace ViewDataAndPrint
{
    public partial class PrePrint : Form
    {
        public bool promiseMode = false;
        public Main main;
        string ID = string.Empty;
        string printer = string.Empty;
        string imgServer = string.Empty;

        public PrePrint(string _ID, string _printer, string _imgServer, Main _m)
        {
            ID = _ID;
            printer = _printer;
            imgServer = _imgServer;
            main = _m;
            InitializeComponent();
            this.Size = new System.Drawing.Size(420, 594); //我也不知道为什么要加，不加就显示不正常
        }

        EntityManager manager;
        EntityBase result;
        private void PrePrint_Load(object sender, EventArgs e)
        {
            manager = new EntityManager();
            RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                result = new EntityBase("CAR_RESULT");
                result["ID"] = ID;
                manager.GetEntity(ref result);
                lblPassTime.Text = DateTime.Parse(result["PASSTIME"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                lblID.Text = ID;
                lblPlate.Text = result["CAR_PLATE"].ToString();
                lblSpeed.Text = result["SPEED"].ToString();
                lblLenght.Text = Math.Round(double.Parse(result["LENGHT"].ToString()) / 100, 3).ToString("0.000");
                //lblWidth.Text = Math.Round(double.Parse(result["WIDTH"].ToString()) / 100, 3).ToString("0.000");
                lblWidth.Text = double.Parse(result["WIDTH"].ToString()) < 2.5 ? "小于2.5米" : Math.Round(double.Parse(result["WIDTH"].ToString()) / 100, 3).ToString("0.000");
                lblHeight.Text = double.Parse(result["HEIGHT"].ToString()) < 4 ? "小于4米" : Math.Round(double.Parse(result["HEIGHT"].ToString()) / 100, 3).ToString("0.000");
                //pB_Img.ImageLocation = result["IMG_PATH"].ToString().Replace("D:", string.Format("\\\\{0}", imgServer));
                //pB_Plate.ImageLocation = result["IMG_PATH"].ToString().Replace("D:", string.Format("\\\\{0}", imgServer)).Replace(".jpg", "~.jpg");
                //pB_Img.ImageLocation = result["IMG_PATH"].ToString().Replace("D:", string.Format("{0}", imgServer));
                //pB_Plate.ImageLocation = result["IMG_PATH"].ToString().Replace("D:", string.Format("{0}", imgServer)).Replace(".jpg", "~.jpg");
                pB_Img.ImageLocation = result["IMG_PATH"].ToString().Replace("D:", string.Format("{0}", imgServer));
                pB_Plate.ImageLocation = result["IMG_PATH"].ToString().Replace("D:", string.Format("{0}", imgServer)).Replace(".jpg", "~.jpg");
            }
            catch
            {
                throw;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // printDocument1 为 打印控件
                //设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型
                this.printDoc1.DefaultPageSettings.PaperSize = new PaperSize("A4", 840, 1188);
                this.printDoc1.PrinterSettings.PrinterName = printer;
                this.printDoc1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage1);
                this.printDoc1.Print();

                this.printDoc2.DefaultPageSettings.PaperSize = new PaperSize("A4", 840, 1188);
                this.printDoc2.PrinterSettings.PrinterName = printer;
                this.printDoc2.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage2);
                this.printDoc2.Print();
                ////显示打印预览
                //printPreviewDialog1.Document = printDoc;
                //DialogResult result = printPreviewDialog1.ShowDialog();
                //if (result == DialogResult.OK)
                //    this.printDocument1.Print();

                result.SetNothing();
                result["ID"] = ID;
                result["IS_PRINT"] = 1;
                manager.UpdateEntity(result);
                main.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MyPrintDocument_PrintPage1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen pen = new Pen(System.Drawing.Brushes.Black);
            Font titleFont = new Font(new FontFamily("黑体"), 28);
            Font bodyFont = new Font(new FontFamily("宋体"), 18);
            /*如果需要改变自己 可以在new Font(new FontFamily("黑体"),11）中的“黑体”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue , 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置 ，第一个10是左边距，第二个35是上边距*/
            //e.Graphics.DrawString("S101省道平湖超限运输检测站", titleFont, System.Drawing.Brushes.Black, 148, 103);
            e.Graphics.DrawString("S101省道K115+250", titleFont, System.Drawing.Brushes.Black, 244, 69);
            e.Graphics.DrawString("超限车辆行驶状态检测单", titleFont, System.Drawing.Brushes.Black, 180, 108);
            //外边框
            e.Graphics.DrawLine(pen, 45, 199, 45, 976);
            e.Graphics.DrawLine(pen, 45, 199, 795, 199);
            e.Graphics.DrawLine(pen, 795, 199, 795, 976);
            e.Graphics.DrawLine(pen, 45, 976, 795, 976);
            //内横线
            e.Graphics.DrawLine(pen, 45, 279, 795, 279);
            e.Graphics.DrawLine(pen, 45, 359, 795, 359);
            e.Graphics.DrawLine(pen, 45, 439, 795, 439);
            e.Graphics.DrawLine(pen, 45, 519, 795, 519);
            //内竖线
            e.Graphics.DrawLine(pen, 204, 199, 204, 519);
            e.Graphics.DrawLine(pen, 483, 199, 483, 519);
            e.Graphics.DrawLine(pen, 622, 199, 622, 519);
            //默认值
            e.Graphics.DrawString("上海至杭州方向", bodyFont, System.Drawing.Brushes.Black, 56, 165);
            e.Graphics.DrawString("检测时间", bodyFont, System.Drawing.Brushes.Black, 60, 227);
            e.Graphics.DrawString("检测单号", bodyFont, System.Drawing.Brushes.Black, 60, 307);
            e.Graphics.DrawString("车牌", bodyFont, System.Drawing.Brushes.Black, 60, 387);
            e.Graphics.DrawString("车速(KM/H)", bodyFont, System.Drawing.Brushes.Black, 60, 467);
            e.Graphics.DrawString("车长(M)", bodyFont, System.Drawing.Brushes.Black, 495, 227);
            e.Graphics.DrawString("车宽(M)", bodyFont, System.Drawing.Brushes.Black, 495, 307);
            e.Graphics.DrawString("车高(M)", bodyFont, System.Drawing.Brushes.Black, 495, 387);
            e.Graphics.DrawString("超限率(%)", bodyFont, System.Drawing.Brushes.Black, 495, 467);
            e.Graphics.DrawString("路政签名:", bodyFont, System.Drawing.Brushes.Black, 52, 1027);
            e.Graphics.DrawString("驾驶员签名:", bodyFont, System.Drawing.Brushes.Black, 413, 1027);
            e.Graphics.DrawLine(pen, 171, 1060, 397, 1060);
            e.Graphics.DrawLine(pen, 557, 1060, 783, 1060);

            //值填充
            e.Graphics.DrawString(lblPassTime.Text, bodyFont, System.Drawing.Brushes.Black, 214, 227);
            e.Graphics.DrawString(lblID.Text, bodyFont, System.Drawing.Brushes.Black, 214, 307);
            e.Graphics.DrawString(lblPlate.Text, bodyFont, System.Drawing.Brushes.Black, 214, 387);
            e.Graphics.DrawString(lblSpeed.Text, bodyFont, System.Drawing.Brushes.Black, 214, 467);
            e.Graphics.DrawString(lblLenght.Text, bodyFont, System.Drawing.Brushes.Black, 632, 227);
            e.Graphics.DrawString(lblWidth.Text, bodyFont, System.Drawing.Brushes.Black, 632, 307);
            e.Graphics.DrawString(lblHeight.Text, bodyFont, System.Drawing.Brushes.Black, 632, 387);
            e.Graphics.DrawString(txtOverLimit.Text, bodyFont, System.Drawing.Brushes.Black, 632, 467);
            e.Graphics.DrawImage(pB_Img.Image, 55, 529, 730, 438);
            e.Graphics.DrawImage(pB_Plate.Image, 55, 529, 180, 144);

            e.Graphics.DrawString("第一联  检测联", new Font(new FontFamily("黑体"), 12), System.Drawing.Brushes.Black, 650, 170);
        }

        private void MyPrintDocument_PrintPage2(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen pen = new Pen(System.Drawing.Brushes.Black);
            Font titleFont = new Font(new FontFamily("黑体"), 28);
            Font bodyFont = new Font(new FontFamily("宋体"), 18);
            /*如果需要改变自己 可以在new Font(new FontFamily("黑体"),11）中的“黑体”改成自己要的字体就行了，黑体 后面的数字代表字体的大小
             System.Drawing.Brushes.Blue , 170, 10 中的 System.Drawing.Brushes.Blue 为颜色，后面的为输出的位置 ，第一个10是左边距，第二个35是上边距*/
            e.Graphics.DrawString("S101省道K115+250", titleFont, System.Drawing.Brushes.Black, 244, 69);
            e.Graphics.DrawString("超限车辆行驶状态检测单", titleFont, System.Drawing.Brushes.Black, 180, 108);
            //外边框
            e.Graphics.DrawLine(pen, 45, 199, 45, 976);
            e.Graphics.DrawLine(pen, 45, 199, 795, 199);
            e.Graphics.DrawLine(pen, 795, 199, 795, 976);
            e.Graphics.DrawLine(pen, 45, 976, 795, 976);
            //内横线
            e.Graphics.DrawLine(pen, 45, 279, 795, 279);
            e.Graphics.DrawLine(pen, 45, 359, 795, 359);
            e.Graphics.DrawLine(pen, 45, 439, 795, 439);
            e.Graphics.DrawLine(pen, 45, 519, 795, 519);
            //内竖线
            e.Graphics.DrawLine(pen, 204, 199, 204, 519);
            e.Graphics.DrawLine(pen, 483, 199, 483, 519);
            e.Graphics.DrawLine(pen, 622, 199, 622, 519);
            //默认值
            e.Graphics.DrawString("上海至杭州方向", bodyFont, System.Drawing.Brushes.Black, 56, 165);
            e.Graphics.DrawString("检测时间", bodyFont, System.Drawing.Brushes.Black, 60, 227);
            e.Graphics.DrawString("检测单号", bodyFont, System.Drawing.Brushes.Black, 60, 307);
            e.Graphics.DrawString("车牌", bodyFont, System.Drawing.Brushes.Black, 60, 387);
            e.Graphics.DrawString("车速(KM/H)", bodyFont, System.Drawing.Brushes.Black, 60, 467);
            e.Graphics.DrawString("车长(M)", bodyFont, System.Drawing.Brushes.Black, 495, 227);
            e.Graphics.DrawString("车宽(M)", bodyFont, System.Drawing.Brushes.Black, 495, 307);
            e.Graphics.DrawString("车高(M)", bodyFont, System.Drawing.Brushes.Black, 495, 387);
            e.Graphics.DrawString("超限率(%)", bodyFont, System.Drawing.Brushes.Black, 495, 467);
            e.Graphics.DrawString("路政签名:", bodyFont, System.Drawing.Brushes.Black, 52, 1027);
            e.Graphics.DrawString("驾驶员签名:", bodyFont, System.Drawing.Brushes.Black, 413, 1027);
            e.Graphics.DrawLine(pen, 171, 1060, 397, 1060);
            e.Graphics.DrawLine(pen, 557, 1060, 783, 1060);

            //值填充
            e.Graphics.DrawString(lblPassTime.Text, bodyFont, System.Drawing.Brushes.Black, 214, 227);
            e.Graphics.DrawString(lblID.Text, bodyFont, System.Drawing.Brushes.Black, 214, 307);
            e.Graphics.DrawString(lblPlate.Text, bodyFont, System.Drawing.Brushes.Black, 214, 387);
            e.Graphics.DrawString(lblSpeed.Text, bodyFont, System.Drawing.Brushes.Black, 214, 467);
            e.Graphics.DrawString(lblLenght.Text, bodyFont, System.Drawing.Brushes.Black, 632, 227);
            e.Graphics.DrawString(lblWidth.Text, bodyFont, System.Drawing.Brushes.Black, 632, 307);
            e.Graphics.DrawString(lblHeight.Text, bodyFont, System.Drawing.Brushes.Black, 632, 387);
            e.Graphics.DrawString(txtOverLimit.Text, bodyFont, System.Drawing.Brushes.Black, 632, 467);
            e.Graphics.DrawImage(pB_Img.Image, 55, 529, 730, 438);
            e.Graphics.DrawImage(pB_Plate.Image, 55, 529, 180, 144);

            e.Graphics.DrawString("第二联  存档联", new Font(new FontFamily("黑体"), 12), System.Drawing.Brushes.Black, 650, 170);
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblPromise_Click(object sender, EventArgs e)
        {
            try
            {
                if (!promiseMode)
                {
                    GetPromise gp = new GetPromise(this, manager);
                    gp.Show();
                }
                else
                {
                    MessageBox.Show("您已经获得授权!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetPromise()
        {
            try
            {
                promiseMode = true;
                this.lblPromise.BackColor = Color.Green;
                this.lblPromise.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 移动点
        /// </summary>
        private Point offset;//移动点
      
        private void PrePrint_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;

            Point cur = this.PointToScreen(e.Location);
            offset = new Point(cur.X - this.Left, cur.Y - this.Top);
        }

        private void PrePrint_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;

            Point cur = MousePosition;
            this.Location = new Point(cur.X - offset.X, cur.Y - offset.Y);
        }

        private void lblPlate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!promiseMode) { throw new Exception("未授权管理员模式!"); }
                ChangeValue cv = new ChangeValue(this, manager, ID, "CAR_PLATE");
                cv.Show();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblSpeed_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!promiseMode) { throw new Exception("未授权管理员模式!"); }
                ChangeValue cv = new ChangeValue(this, manager, ID, "SPEED");
                cv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblLenght_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!promiseMode) { throw new Exception("未授权管理员模式!"); }
                ChangeValue cv = new ChangeValue(this, manager, ID, "LENGHT");
                cv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblWidth_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!promiseMode) { throw new Exception("未授权管理员模式!"); }
                ChangeValue cv = new ChangeValue(this, manager, ID, "WIDTH");
                cv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblHeight_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!promiseMode) { throw new Exception("未授权管理员模式!"); }
                ChangeValue cv = new ChangeValue(this, manager, ID, "HEIGHT");
                cv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
