using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseProcessing;
using System.IO;
using System.Drawing.Drawing2D;

namespace CreateImgStream
{
    public partial class Main : Form
    {
        class SensorTimeSpan
        {
            public int ID;
            public int StartTime;
            public int EndTime;
            public DateTime PassTime;
        }

        int cut12 = 71;
        int cut23 = 142;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private string CreateImg(string startId, string endId, string foldPath)
        {
            try
            {
                EntityManager manager = new EntityManager();

                //开始id等于结束id，直接返回
                if (startId.Equals(endId)) { throw new Exception("NO DATA!"); }

                //获取传感器数据
                EntityBase sensorDataEntity = new EntityBase("V_SENSOR_DATA");
                WhereObjectList dataWhere = new WhereObjectList();
                //dataWhere.add("OUT_FLAG", WhereObjectType.NotEqualTo, "0"); //不等于0，包含了1和NULL
                dataWhere.add("LOGID", WhereObjectType.GreaterThanOrEqualTo, startId);
                dataWhere.addOr(new WhereObject("IN_VALUE", WhereObjectType.LessThan, "5300"), new WhereObject("OUT_VALUE", WhereObjectType.LessThan, "5300"));
                dataWhere.add("LOGID", WhereObjectType.LessThanOrEqualTo, endId);
                DataTable sensorDataTable = manager.GetTableEx(sensorDataEntity, dataWhere, 0, "IN_SENSORTIME");

                if (sensorDataTable.Rows.Count.Equals(0)) { throw new Exception("NO DATA!"); }

                int rowCount = sensorDataTable.Rows.Count;
                int minTimeValue = int.Parse(sensorDataTable.Rows[0]["IN_SENSORTIME"].ToString());
                int maxTimeValue;
                int maxTimeValue1 = int.Parse(sensorDataTable.Rows[rowCount - 1]["IN_SENSORTIME"].ToString());
                int maxTimeValue2 = 0;
                for (int i = 1; i < rowCount; i++)
                {
                    if (!(sensorDataTable.Rows[rowCount - i]["OUT_SENSORTIME"] is DBNull))
                    {
                        maxTimeValue2 = int.Parse(sensorDataTable.Rows[rowCount - i]["OUT_SENSORTIME"].ToString());
                        break;
                    }
                }
                maxTimeValue = maxTimeValue1 > maxTimeValue2 ? maxTimeValue1 : maxTimeValue2;

                //(254*2 + 200)
                Bitmap img = new Bitmap(700, ((maxTimeValue - minTimeValue) + 200));
                Graphics g = Graphics.FromImage(img);//图像 
                g.FillRectangle(Brushes.White, new Rectangle(0, 0, 700, ((maxTimeValue - minTimeValue) + 200)));

                string fileName = string.Format("ID_{0}-{1}.png", startId, endId);
                string imgPath = string.Format("{0}\\{1}", foldPath, fileName);
                Pen pBlack = new Pen(Color.Black, 1);
                Pen gp = new Pen(Color.GreenYellow, 1);
                Pen rp = new Pen(Color.Red, 1);
                //从100，100 开始画
                int lenghtOffset = ((minTimeValue / 100) * 100) - 100;
                int widthOffset = -100;

                for (int i = 1; i < 255; i++)
                {
                    if (i % 10 == 0)
                    {
                        g.DrawLine(gp, i * 2 - widthOffset, 0, i * 2 - widthOffset, ((maxTimeValue - minTimeValue) + 200));
                    }
                }

                for (int i = minTimeValue; i <= maxTimeValue; i += 100)
                {
                    int stringValue = (i / 100) * 100;
                    int y_stringValue = (i - minTimeValue) / 100;
                    y_stringValue *= 100;
                    g.DrawLine(rp, 0, y_stringValue + 100, 700, y_stringValue + 100);
                    g.DrawString(stringValue.ToString(), new Font("宋体", 8), Brushes.Red, 5, y_stringValue + 102);
                }

                g.DrawLine(rp, 15 * 2 - widthOffset + 1, 0, 15 * 2 - widthOffset + 1, ((maxTimeValue - minTimeValue) + 200));
                g.DrawString("15/16", new Font("宋体", 8), Brushes.Red, 15 * 2 - widthOffset + 3, 2);
                g.DrawLine(rp, cut12 * 2 - widthOffset - 1, 0, cut12 * 2 - widthOffset - 1, ((maxTimeValue - minTimeValue) + 200));
                g.DrawString("71/72", new Font("宋体", 8), Brushes.Red, 66 * 2 - widthOffset + 3, 2);
                g.DrawLine(rp, cut23 * 2 - widthOffset - 1, 0, cut23 * 2 - widthOffset - 1, ((maxTimeValue - minTimeValue) + 200));
                g.DrawString("142/143", new Font("宋体", 8), Brushes.Red, 151 * 2 - widthOffset + 3, 2);
                g.DrawLine(rp, 239 * 2 - widthOffset + 1, 0, 239 * 2 - widthOffset + 1, ((maxTimeValue - minTimeValue) + 200));
                g.DrawString("239/240", new Font("宋体", 8), Brushes.Red, 239 * 2 - widthOffset + 3, 2);

                List<int> addTimeList = new List<int>();
                //foreach (DataRow dr in sensorDataTable.Rows)
                //{
                //    int id = int.Parse(dr["ID"].ToString());
                //    int outFlag = dr["OUT_FLAG"] is DBNull ? 0 : int.Parse(dr["OUT_FLAG"].ToString());
                //    int in_Time = int.Parse(dr["IN_SENSORTIME"].ToString());
                //    int out_Time = outFlag == 0 ? 0 : int.Parse(dr["OUT_SENSORTIME"].ToString());
                //    int in_Value = int.Parse(dr["IN_VALUE"].ToString());
                //    int out_Value = outFlag == 0 ? 0 : int.Parse(dr["OUT_VALUE"].ToString());
                //    int value = in_Value > out_Value & out_Value != 0 ? (5800 - out_Value) : (5800 - in_Value);

                //    float x_set = id * 2 - widthOffset;
                //    float y_set1 = in_Time - lenghtOffset;
                //    float y_set2 = out_Time - lenghtOffset;

                //    if (out_Time != 0)
                //    {
                //        g.DrawLine(pBlack, x_set, y_set1, x_set, y_set2);
                //    }
                //    else
                //    {
                //        g.DrawLine(pBlack, x_set, y_set1, x_set, y_set1);
                //    }

                //    int y_stringValue = (in_Time - minTimeValue) / 100;
                //    y_stringValue *= 100;
                //    if (!addTimeList.Contains(y_stringValue))
                //    {
                //        g.DrawString(dr["IN_PASSTIME"].ToString(), new Font("宋体", 8), Brushes.Red, 5, y_stringValue + 112);
                //        addTimeList.Add(y_stringValue);
                //    }
                //}

                ///////////////////////////////
                List<SensorTimeSpan> listSensorTimeSpan = new List<SensorTimeSpan>();

                //所有传感器最小值
                int minSensorTime = (int)sensorDataTable.Rows[0]["IN_SENSORTIME"];
                //所有传感器最大值
                int maxSensorTime = 0;
                foreach (DataRow row in sensorDataTable.Rows)
                {
                    int sensorId = (int)row["ID"];
                    int rowStartSensorTime = (int)row["IN_SENSORTIME"];
                    int outFlag = row["OUT_FLAG"] is DBNull ? 0 : int.Parse(row["OUT_FLAG"].ToString());
                    int rowEndSensorTime = outFlag == 1 ? (int)row["OUT_SENSORTIME"] : (int)row["IN_SENSORTIME"];
                    DateTime carPassTime = (DateTime)row["IN_PASSTIME"];

                    maxSensorTime = rowEndSensorTime > maxSensorTime ? rowEndSensorTime : maxSensorTime;

                    if (listSensorTimeSpan.Count(x => x.ID == sensorId) > 0)
                    {
                        var existSTSs = listSensorTimeSpan.Where(x => x.ID == sensorId);
                        bool isMerge = false;
                        foreach (SensorTimeSpan existSTS in existSTSs)
                        {
                            if ((rowStartSensorTime - 200) < existSTS.EndTime)
                            {
                                listSensorTimeSpan.Remove(existSTS);
                                existSTS.EndTime = rowEndSensorTime;
                                listSensorTimeSpan.Add(existSTS);
                                isMerge = true;
                                break;
                            }
                        }
                        if (!isMerge)
                        {
                            SensorTimeSpan newSTS = new SensorTimeSpan();
                            newSTS.ID = sensorId;
                            newSTS.StartTime = rowStartSensorTime;
                            newSTS.EndTime = rowEndSensorTime;
                            newSTS.PassTime = carPassTime;
                            listSensorTimeSpan.Add(newSTS);
                        }
                    }
                    else
                    {
                        SensorTimeSpan newSTS = new SensorTimeSpan();
                        newSTS.ID = sensorId;
                        newSTS.StartTime = rowStartSensorTime;
                        newSTS.EndTime = rowEndSensorTime;
                        newSTS.PassTime = carPassTime;
                        listSensorTimeSpan.Add(newSTS);
                    }
                }
                //排序方便画图
                listSensorTimeSpan.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
                foreach (SensorTimeSpan existSTS in listSensorTimeSpan)
                {
                    int id = existSTS.ID;
                    int in_Time = existSTS.StartTime;
                    int out_Time = existSTS.EndTime;

                    float x_set = id * 2 - widthOffset;
                    float y_set1 = in_Time - lenghtOffset;
                    float y_set2 = out_Time - lenghtOffset;

                    if (out_Time != 0)
                    {
                        g.DrawLine(pBlack, x_set, y_set1, x_set, y_set2);
                    }
                    else
                    {
                        g.DrawLine(pBlack, x_set, y_set1, x_set, y_set1);
                    }

                    int y_stringValue = (in_Time - minTimeValue) / 100;
                    y_stringValue *= 100;
                    if (!addTimeList.Contains(y_stringValue))
                    {
                        g.DrawString(existSTS.PassTime.ToString(), new Font("宋体", 8), Brushes.Red, 5, y_stringValue + 112);
                        addTimeList.Add(y_stringValue);
                    }
                }
                //////////////////////////////

                Bitmap bmp = new Bitmap(img);
                g.Dispose();
                img.Dispose();
                bmp.Save(@imgPath);
                bmp.Dispose();

                return fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int startId = int.Parse(txtStart.Text);
                int endId = int.Parse(txtEnd.Text);
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string foldPath = dialog.SelectedPath;
                    string fileName = CreateImg(startId.ToString(), endId.ToString(), foldPath);
                    MessageBox.Show(fileName + " 已保存!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
