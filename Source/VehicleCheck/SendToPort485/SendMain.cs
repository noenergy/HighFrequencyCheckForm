using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SendToPort485
{
    public partial class SendMain : Form
    {
        public SendMain()
        {
            InitializeComponent();
            this.Visible = false;
            this.Hide();
        }

        private void SendMain_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Hide();
            try
            {
                if (!this.serialPort1.IsOpen)
                {
                    this.serialPort1.Open();
                }
                Byte[] BSendTemp = new Byte[8];
                BSendTemp[0] = 0xAA;
                BSendTemp[1] = 0x06;
                BSendTemp[2] = 0x00;
                BSendTemp[3] = 0x01;
                BSendTemp[4] = 0x00;
                BSendTemp[5] = 0x03;
                BSendTemp[6] = 0x81;
                BSendTemp[7] = 0xD0;
                this.serialPort1.Write(BSendTemp, 0, 8);//发送数据

                this.serialPort1.Close();
                System.Threading.Thread.Sleep(1000);
                if (this.serialPort1.IsOpen)
                {
                    this.serialPort1.Close();
                }
                this.serialPort1.Dispose();
            }
            catch (Exception ex)
            {
                if (this.serialPort1.IsOpen)
                {
                    this.serialPort1.Close();
                    this.serialPort1.Dispose();
                }
                if (MessageBox.Show(ex.Message + " 是否重启?", "重启确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Process.Start("shutdown.exe", "-r -t 0");
                }
                //Process.Start("shutdown.exe", "-r -t 0");
            }
            this.Close();
    }
}
}
