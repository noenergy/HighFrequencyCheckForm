using System;
using System.Collections.Generic;
using System.Data;
using DatabaseProcessing;

namespace BackstageRun
{
    /// <summary>
    /// 数据库交互            初始化后每次存储时间约为2ms左右
    /// </summary>
    public class DataBase
    {
        //本地路径
        static string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        EntityManager manager;

        public  DataBase()
        {
            manager = new EntityManager(); 
        }

        /// <summary>
        /// 获取后台配置
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, BackstageRunConfig> GetConfig()
        {
            try
            {
                EntityBase configEntity = new EntityBase("BACKSTAGERUNCONFIG");
                DataTable dt = manager.GetTable(configEntity, 0, "");
                
                Dictionary<string, BackstageRunConfig> Ret =
                   new Dictionary<string, BackstageRunConfig>();
                foreach (DataRow dr in dt.AsEnumerable())
                {
                    BackstageRunConfig Config = new BackstageRunConfig();

                    Config.BR_ID = dr["BR_ID"].ToString();
                    Config.BR_DESC = dr["BR_DESC"].ToString();
                    Config.BR_TYPE = Convert .ToInt16(dr["BR_TYPE"]);
                    Config.PARANUM = Convert.ToInt16(dr["PARANUM"]);
                    Config.DLLNAME = dr["DLLNAME"].ToString();
                    Config.NAMESPACE_CLASS = dr["NAMESPACE_CLASS"].ToString();
                    Config.FUNCTION_NAME = dr["FUNCTION_NAME"].ToString();
                    Config.STEPTIME = Convert.ToInt16(dr["STEPTIME"]);

                    Ret.Add(dr["BR_ID"].ToString(), Config);
                }
                dt.Dispose();

                return Ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新任务配置
        /// </summary>
        /// <param name="config"></param>
        public void UpdateConfig(BackstageRunConfig config)
        {
            try
            {
                EntityBase configEntity = new EntityBase("BACKSTAGERUNCONFIG");
                configEntity["BR_DESC"] = config.BR_DESC;
                configEntity["BR_TYPE"] = config.BR_TYPE;
                configEntity["PARANUM"] = config.PARANUM;
                configEntity["DLLNAME"] = config.DLLNAME;
                configEntity["NAMESPACE_CLASS"] = config.NAMESPACE_CLASS;
                configEntity["FUNCTION_NAME"] = config.FUNCTION_NAME;
                configEntity["STEPTIME"] = config.STEPTIME;
                configEntity["BR_ID"] = config.BR_ID;
                manager.UpdateEntity(configEntity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 增加任务配置
        /// </summary>
        /// <param name="config"></param>
        public void AddConfig(BackstageRunConfig config)
        {
            try
            {
                EntityBase configEntity = new EntityBase("BACKSTAGERUNCONFIG");
                configEntity["BR_DESC"] = config.BR_DESC;
                configEntity["BR_TYPE"] = config.BR_TYPE;
                configEntity["PARANUM"] = config.PARANUM;
                configEntity["DLLNAME"] = config.DLLNAME;
                configEntity["NAMESPACE_CLASS"] = config.NAMESPACE_CLASS;
                configEntity["FUNCTION_NAME"] = config.FUNCTION_NAME;
                configEntity["STEPTIME"] = config.STEPTIME;
                configEntity["BR_ID"] = config.BR_ID;
                manager.AddNewEntity(ref configEntity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除任务配置
        /// </summary>
        /// <param name="Config"></param>
        public void DeleteConfig(string br_Id)
        {
            try
            {
                //删除任务配置
                EntityBase configEntity = new EntityBase("BACKSTAGERUNCONFIG");
                EntityBase taskEntity = new EntityBase("BACKSTAGERUNTASK");
                configEntity["BR_ID"] = br_Id;
                manager.DeleteEntity(configEntity);
                //删除未完成的任务
                WhereObjectList where = new WhereObjectList();
                where.add("BR_ID", WhereObjectType.EqualTo, br_Id);
                where.add("stATUS", WhereObjectType.EqualTo, "0");
                manager.DeleteEntityEx(taskEntity, where);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public List<Task> GetTask()
        {
            try
            {
                EntityBase taskEntity = new EntityBase("BACKSTAGERUNTASK");
                WhereObjectList where = new WhereObjectList();
                where.add("STATUS", WhereObjectType.EqualTo, "0");
                DataTable dt = manager.GetTableEx(taskEntity, where, 0, "");

                List<Task> TaskList = new List<Task>();
                foreach (DataRow dr in dt.AsEnumerable())
                {
                    Task t = new Task();
                    t.TASKID = dr["TASKID"].ToString();
                    t.BR_ID = dr["BR_ID"].ToString();
                    t.DATA1 = dr["DATA1"].ToString();
                    t.DATA2 = dr["DATA2"].ToString();
                    t.DATA3 = dr["DATA3"].ToString();
                    t.DATA4 = dr["DATA4"].ToString();
                    t.DATA5 = dr["DATA5"].ToString();
                    t.DATA6 = dr["DATA6"].ToString();
                    t.DATA7 = dr["DATA7"].ToString();
                    t.BEGINDATE = dr["BEGINDATE"].ToString();
                    t.CREATEDATE = dr["CREATEDATE"].ToString();

                    TaskList.Add(t);
                }
                dt.Dispose();

                return TaskList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetTask(string status)
        {
            try
            {
                EntityBase taskEntity = new EntityBase("BACKSTAGERUNTASK");
                WhereObjectList where = new WhereObjectList();
                where.add("STATUS", WhereObjectType.EqualTo, status);
                DataTable dt = manager.GetTableEx(taskEntity, where, 0, "TASKID");
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 写入后台任务
        /// </summary>
        /// <param name="task"></param>
        public void WriteTask(Task task)
        {
            try
            {
                EntityBase taskEntity = new EntityBase("BACKSTAGERUNTASK");
                taskEntity["BR_ID"] = task.BR_ID;
                taskEntity["DATA1"] = task.DATA1;
                taskEntity["DATA2"] = task.DATA2;
                taskEntity["DATA3"] = task.DATA3;
                taskEntity["DATA4"] = task.DATA4;
                taskEntity["DATA5"] = task.DATA5;
                taskEntity["DATA6"] = task.DATA6;
                taskEntity["DATA7"] = task.DATA7;
                taskEntity["STATUS"] = 0;
                if (!task.BEGINDATE.Equals(string.Empty)) { taskEntity["BEGINDATE"] = task.BEGINDATE; }
                taskEntity["CREATEDATE"] = DateTime.Now;
                manager.AddNewEntity(ref taskEntity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除后台任务
        /// </summary>
        /// <param name="task"></param>
        public void DeleteTask(string taskId)
        {
            try
            {
                EntityBase taskEntity = new EntityBase("BACKSTAGERUNTASK");
                taskEntity["TASKID"] = taskId;
                manager.DeleteEntity(taskEntity);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 开始任务记录
        /// </summary>
        /// <param name="taskId"></param>
        public void StartTask(string taskId)
        {
            try
            {
                EntityBase taskEntity = new EntityBase("BACKSTAGERUNTASK");
                taskEntity["TASKID"] = taskId;
                taskEntity["STATUS"] = 1;
                taskEntity["BEGINDATE"] = DateTime.Now;
                manager.UpdateEntity(taskEntity);
            }
            catch
            {
                //无操作
            }
        }

        /// <summary>
        /// 结束任务记录
        /// </summary>
        /// <param name="status"></param>
        /// <param name="taskId"></param>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndTask(string status, string taskId,string message,string result)
        {
            try
            {
                EntityBase taskEntity = new EntityBase("BACKSTAGERUNTASK");
                taskEntity["TASKID"] = taskId;
                taskEntity["STATUS"] = status;
                taskEntity["ENDDATE"] = DateTime.Now;
                taskEntity["MESSAGE"] = message;
                taskEntity["RESULT"] = result;
                manager.UpdateEntity(taskEntity);
            }
            catch
            {
                //无操作
            }
        }
    }
}
