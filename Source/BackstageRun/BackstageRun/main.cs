using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace BackstageRun
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private bool isRunStatusSqlConnectError = false;
        private int canNotConnectDBWaitTime = 90;
        private int nowWaitConnectAgainTime = 90;
        //DataSet ds = new DataSet();
        private DataBase DB;
        private Dictionary<string, BackstageRunConfig> taskConfig;
        delegate void AddScreenLog(); //定义委托类型

        private void main_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!File.Exists("setting.xml"))
                //{
                //    throw new Exception("未找到设置文件");
                //}

                //ds.ReadXml("setting.xml");

                //var tasks = from ret in ds.Tables[0].AsEnumerable() where ret["ISUSE"].ToString() != "0" select ret;

                //foreach (DataRow task in ds.Tables[0].Rows)
                //{
                //    if(!File .Exists (task["DLL"].ToString()))
                //    {
                //        throw new Exception("未找到运行文件:" + task["DLL"].ToString());
                //    }
                //}

                //taskTable = ds.Tables[0];
                //Cmd("ping www.baidu.com");

                try
                {
                    DB = new DataBase();
                }
                catch (Exception ex)
                {
                    txtLog.AppendText(string.Format("\r\n{0}\r\nBackstageRun Database Connection Config error!\r\n{1}\r\n", DateTime.Now, ex.Message));
                }

                //默认开启任务栏图标
                this.notifyIconButton.Visible = true;
                contextMenuStrip4NotifyIcon.Items["tsmStart"].Enabled = true;
                contextMenuStrip4NotifyIcon.Items["tsmStop"].Enabled = false;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnAddTask.Enabled = true;

                //默认打开就运行
                btnStart_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        public void Cmd(string c)
        {            
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.Start();

            process.StandardInput.WriteLine(c + "&exit");
            process.StandardInput.AutoFlush = true;
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令
            process.Refresh();
            StreamReader reader = process.StandardOutput;//截取输出流

            string output = reader.ReadToEnd();

            //string output = reader.ReadLine();//每次读取一行

            //while (!reader.EndOfStream)
            //{
            //    output = reader.ReadLine();
            //}

            process.WaitForExit();
            process.Close();
            MessageBox.Show(output);
        }

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (isRunStatusSqlConnectError && nowWaitConnectAgainTime > 0)
                {
                    txtLog.AppendText(string.Format("\r\nWait for Connect Database Retry - {0} s", nowWaitConnectAgainTime));
                    nowWaitConnectAgainTime--;
                }
                else
                {
                    isRunStatusSqlConnectError = false;

                    if (txtLog.Lines.Length == 300)
                    {
                        txtLog.Clear();
                    }
                    List<Task> taskList = DB.GetTask();
                    foreach (Task task in taskList)
                    {
                        if (taskConfig[task.BR_ID].STEPTIME.Equals(0))
                        {
                            DateTime beginDateTime = Convert.ToDateTime(task.BEGINDATE);
                            DateTime endDateTime = beginDateTime.AddSeconds(3);//误差范围为3秒
                            DateTime nowDateTime = DateTime.Now;
                            if (nowDateTime < beginDateTime || nowDateTime > endDateTime)
                                continue;
                        }
                        else
                        {
                            DateTime createTime = Convert.ToDateTime(task.CREATEDATE);
                            TimeSpan ts = DateTime.Now - createTime;
                            if (ts.TotalSeconds < taskConfig[task.BR_ID].STEPTIME)
                                continue;
                        }

                        txtLog.AppendText(string.Format("\r\n{0}\r\nBackstageRun ready run {1}\r\n", DateTime.Now,
                                    task.BR_ID));

                        Thread eT = new Thread(new ParameterizedThreadStart(ExecuteTask));
                        eT.IsBackground = true;
                        eT.Start(task);
                    }
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText(string.Format("\r\n{0}\r\nBackstageRun timer error!\r\n{1}\r\n", DateTime.Now,
                    ex.Message));
                if (ex.Message.Contains("SQL") || ex.Message.Contains("传输"))
                {
                    isRunStatusSqlConnectError = true;
                    nowWaitConnectAgainTime = canNotConnectDBWaitTime;
                }
            }
        }

        /// <summary>
        /// 线程是否运行
        /// </summary>
        private bool isThreadRun = false;
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="mission"></param>
        private void ExecuteTask(object mission)
        {       
            try
            {
                isThreadRun = true;
                Task task = (Task)mission;
                try
                {
                    DB.StartTask(task.TASKID);
                    if (taskConfig[task.BR_ID].BR_TYPE.Equals(0))
                    {
                        string argStr = string.Format("{0} {1} {2} {3} {4}", taskConfig[task.BR_ID].BR_TYPE, taskConfig[task.BR_ID].DLLNAME, taskConfig[task.BR_ID].NAMESPACE_CLASS, taskConfig[task.BR_ID].FUNCTION_NAME, taskConfig[task.BR_ID].PARANUM);
                        if (!taskConfig[task.BR_ID].PARANUM.Equals(0))
                        {
                            if (task.DATA1.Length > 0) { argStr += string.Format(" {0}", task.DATA1); } 
                            if (task.DATA2.Length > 0) { argStr += string.Format(" {0}", task.DATA2); } 
                            if (task.DATA3.Length > 0) { argStr += string.Format(" {0}", task.DATA3); } 
                            if (task.DATA4.Length > 0) { argStr += string.Format(" {0}", task.DATA4); } 
                            if (task.DATA5.Length > 0) { argStr += string.Format(" {0}", task.DATA5); } 
                            if (task.DATA6.Length > 0) { argStr += string.Format(" {0}", task.DATA6); } 
                            if (task.DATA7.Length > 0) { argStr += string.Format(" {0}", task.DATA7); } 
                        }

                        Process myprocess = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo("TaskProcess.exe", argStr);
                        myprocess.StartInfo = startInfo;
                        myprocess.StartInfo.UseShellExecute = false;
                        myprocess.StartInfo.RedirectStandardOutput = true;  //允许重定向标准输出
                        myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //选择隐藏属性
                        myprocess.StartInfo.CreateNoWindow = true;//不显示程序窗口
                        myprocess.Start();
                        string ret = myprocess.StandardOutput.ReadToEnd();
                        myprocess.WaitForExit();
                        myprocess.Close();
                        myprocess.Dispose();

                        //方法返回值，开头为ERROR为报错信息
                        if (ret.Length > 0)
                        {
                            if (ret.Substring(0, 5).Equals("ERROR"))
                            {
                                throw new Exception(ret.Substring(6));
                            }
                            else
                            {
                                ret = string.Format(", Return: {0}", ret);
                            }
                        }


                        DB.EndTask("2", task.TASKID, "", ret);
                        WriteLog(task.BR_ID, "Run Success" + ret);

                        if (task.BEGINDATE.Equals(string.Empty))
                            DB.WriteTask(task);

                        AddScreenLog addLog = delegate ()
                        {
                            txtLog.AppendText(string.Format("\r\n{0}\r\nBackstageRun run {1} success{2}\r\n", DateTime.Now,
                                                        task.BR_ID, ret));
                        };
                        txtLog.Invoke(addLog);
                    }
                    else
                    {
                        string argStr = string.Format("{0} {1} {2}", taskConfig[task.BR_ID].BR_TYPE, taskConfig[task.BR_ID].DLLNAME, taskConfig[task.BR_ID].NAMESPACE_CLASS);
                        Process myprocess = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo("TaskProcess.exe", argStr);
                        myprocess.StartInfo = startInfo;
                        myprocess.StartInfo.UseShellExecute = false;
                        myprocess.StartInfo.RedirectStandardOutput = true;  //允许重定向标准输出
                        myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //选择隐藏属性
                        myprocess.StartInfo.CreateNoWindow = true;//不显示程序窗口  //true不显示窗口,但是可能发生程序运行异常终止的问题。
                        myprocess.Start();
                        string ret = myprocess.StandardOutput.ReadToEnd();
                        myprocess.WaitForExit();
                        myprocess.Close();
                        myprocess.Dispose();

                        //窗口方法存在返回值即为报错信息
                        if (ret.Length > 0) { throw new Exception(ret); }
                
                        DB.EndTask("2", task.TASKID, "", "");
                        WriteLog(task.BR_ID, "Open Success");

                        if (task.BEGINDATE.Equals(string.Empty))
                            DB.WriteTask(task);

                        AddScreenLog addLog = delegate ()
                        {
                            txtLog.AppendText(string.Format("\r\n{0}\r\nBackstageRun Open {1} Success\r\n", DateTime.Now,
                                                        task.BR_ID));
                        };
                        txtLog.Invoke(addLog);
                    }
                }
                catch (Exception ex)
                {
                    string run_Open = taskConfig[task.BR_ID].BR_TYPE.Equals(0) ? "Run" : "Open";
                    DB.EndTask("-1", task.TASKID, ex.Message, "");
                    WriteLog(task.BR_ID, string.Format("{0} Error, Message: {1}", run_Open, ex.Message));
                    //循环任务继续增加任务
                    if (task.BEGINDATE.Equals(string.Empty))
                        DB.WriteTask(task);

                    AddScreenLog addLog = delegate ()
                    {
                        txtLog.AppendText(string.Format("\r\n{0}\r\nBackstageRun {1} {2} Error, Message: {3}\r\n", DateTime.Now, run_Open,
                                                    task.BR_ID, ex.Message));
                    };
                    txtLog.Invoke(addLog);
                }
            }
            catch
            {
                //nothing
            }
            finally
            {
                isThreadRun = false;
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="log"></param>
        private void WriteLog(string taskName, string log)
        {
            try
            {
                string dateStr = DateTime.Now.ToString("yyyy-MM-dd");
                string path = string.Format("{0}\\{1}.txt", dateStr, taskName);
                if (File .Exists (path))
                {
                    using (StreamWriter SW = File.AppendText(path))
                    {
                        SW.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + log);
                        SW.Close();
                    }
                }
                else
                {
                    if (!Directory.Exists(dateStr))
                    {
                        Directory.CreateDirectory(dateStr);
                    }
                    StreamWriter SW;
                    SW = File.CreateText(path);
                    SW.WriteLine("Log created at: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    SW.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + log);
                    SW.Close();
                }
            }
            catch (Exception)
            {
                //nothing
            }
        }

#region 窗口事件
        /// <summary>
        /// 是否开始标识符
        /// </summary>
        private bool isStart = false;
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isStart)
                {
                    txtLog.AppendText("\r\n////////////////\r\n");
                    txtLog.AppendText(string.Format("{0}\r\n", DateTime.Now));
                    txtLog.AppendText("BackstageRun is starting...\r\n");

                    taskConfig = DB.GetConfig();

                    txtLog.AppendText("BackstageRun is load config...\r\n");

                    timer.Start();

                    txtLog.AppendText("BackstageRun is started.");
                    txtLog.AppendText("\r\n////////////////\r\n");
                    isStart = true;

                    contextMenuStrip4NotifyIcon.Items["tsmStart"].Enabled = false;
                    contextMenuStrip4NotifyIcon.Items["tsmStop"].Enabled = true;
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    btnAddTask.Enabled = false;
                }
                else
                {
                    txtLog.AppendText(string.Format("\r\n{0}\r\nBackstageRun is already started.\r\n", DateTime.Now));
                }
            }
            catch (Exception ex)
            {
                txtLog.AppendText(string.Format("\r\n{0}\r\nError:{1}\r\n", DateTime.Now, ex.Message));
            }            
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (isStart)
                {
                    txtLog.AppendText("\r\n////////////////\r\n");
                    txtLog.AppendText(string.Format("{0}\r\n", DateTime.Now));
                    txtLog.AppendText("BackstageRun is stoping...\r\n");
                    isStart = false;
                    timer.Stop();
                    txtLog.AppendText("BackstageRun is stoped.");
                    txtLog.AppendText("\r\n////////////////\r\n");
                }
                else
                {
                    txtLog.AppendText(string.Format("\r\n{0}\r\nBackstageRun is already stoped.\r\n", DateTime.Now));
                }
                contextMenuStrip4NotifyIcon.Items["tsmStart"].Enabled = true;
                contextMenuStrip4NotifyIcon.Items["tsmStop"].Enabled = false;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnAddTask.Enabled = true;
            }
            catch (Exception ex)
            {
                txtLog.AppendText(string.Format("\r\n{0}\r\nError:{1}\r\n", DateTime.Now, ex.Message));
            }            
        }
        /// <summary>
        /// 窗口是否最小化
        /// </summary>
        private bool isFormMin = false;
        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMin_Click(object sender, EventArgs e)
        {
            isFormMin = true;
            this.Hide();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (isThreadRun) throw new Exception("当前存在正在运行的线程。请稍后再关闭!");
                DialogResult dr = MessageBox.Show("确认关闭？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    //Recieve.Abort();
                    Application.Exit();
                }
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
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblName_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;

            Point cur = this.PointToScreen(e.Location);
            offset = new Point(cur.X - this.Left, cur.Y - this.Top);
        }
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblName_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;

            Point cur = MousePosition;
            this.Location = new Point(cur.X - offset.X, cur.Y - offset.Y);
        }
        /// <summary>
        /// 状态栏菜单开始按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmStart_Click(object sender, EventArgs e)
        {
            btnStart_Click(sender, e);
        }
        /// <summary>
        /// 状态栏菜单暂停按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmStop_Click(object sender, EventArgs e)
        {
            btnStop_Click(sender, e);
        }
        /// <summary>
        /// 状态栏菜单退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmExit_Click(object sender, EventArgs e)
        {
            BtnClose_Click(sender, e);
        }
        /// <summary>
        /// 双击状态栏图标事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIconButton_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (isFormMin)
                {
                    this.Show();
                    isFormMin = false;
                }
                else
                {
                    this.Hide();
                    isFormMin = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 打开设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            setting s = new setting(DB);
            s.Show();
        }
        /// <summary>
        /// 打开任务添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            try
            {
                if (isStart) throw new Exception("当前服务正在运行，不能增加任务!");
                addtask a = new addtask(DB);
                a.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
#endregion
    }
}
