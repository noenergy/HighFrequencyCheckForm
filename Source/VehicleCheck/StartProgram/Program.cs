using System;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace StartProgram
{
    class Program
    {
        static string getPlatePath = string.Empty;
        static string getUDPPath = string.Empty;
        static string getBackRunPath = string.Empty;
        static void Main(string[] args)
        {
            try
            {
                ReadPrintConfig();
                if (File.Exists(getPlatePath) && File.Exists(getUDPPath) && File.Exists(getBackRunPath))
                {
                    int waitTime = 31;
                    while (waitTime > 1) 
                    { 
                        waitTime--;
                        Console.WriteLine(waitTime.ToString());
                        System.Threading.Thread.Sleep(1000);
                    }
                    Process.Start(getPlatePath);
                    Process.Start(getUDPPath);
                    Process.Start(getBackRunPath);
                }
                else
                {
                    throw new Exception("软件路径错误！");
                }            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        static private void ReadPrintConfig()
        {

            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            XmlDocument doc = new XmlDocument();
            doc.Load(path + "\\StartInfo.config");

            XmlNodeList Node = doc.SelectSingleNode("config").ChildNodes;

            for (int n = 0; n < Node.Count; n++)
            {
                XmlNodeList cNode = Node.Item(n).ChildNodes;
                int nodeNum = cNode.Count;

                if (Node.Item(n).Name.Equals("PLATE"))
                {
                    try
                    {
                        getPlatePath = Node[n].InnerXml;
                    }
                    catch
                    {
                        throw new Exception("车牌信息获取软件路径设置错误！");
                    }
                }
                if (Node.Item(n).Name.Equals("UDP"))
                {
                    try
                    {
                        getUDPPath = Node[n].InnerXml;
                    }
                    catch
                    {
                        throw new Exception("传感器信号获取软件路径设置错误！");
                    }
                }
                if (Node.Item(n).Name.Equals("BACKRUN"))
                {
                    try
                    {
                        getBackRunPath = Node[n].InnerXml;
                    }
                    catch
                    {
                        throw new Exception("后台运行软件路径设置错误！");
                    }
                }
            }
            if (string.IsNullOrEmpty(getPlatePath) || string.IsNullOrEmpty(getUDPPath) || string.IsNullOrEmpty(getBackRunPath)) { throw new Exception("配置信息不完整!"); }
        }
    }
}
