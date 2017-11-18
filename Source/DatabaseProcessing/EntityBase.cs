using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseProcessing
{
    public class EntityBase
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public string source = string.Empty;
        /// <summary>
        /// 查询名称
        /// </summary>
        public string queryName = string.Empty;
        /// <summary>
        /// 成员列表
        /// </summary>
        private List<string> memberList;
        /// <summary>
        /// 数据存储源
        /// </summary>
        private Dictionary<string, object> data = new Dictionary<string, object>();

        /// <summary>
        /// 数据实例
        /// </summary>
        /// <param name="_dataSource">数据源</param>
        public EntityBase(string _dataSource)
        {
            source = _dataSource;
            //存在数据源则获取成员列表
            if (source.Trim().Length > 0)
            {
                memberList = new EntityManager().GetMemberList(source);
            }
        }

        /// <summary>
        /// 数据实例
        /// </summary>
        /// <param name="_dataSource">数据源</param>
        /// <param name="_queryName">查询名称</param>
        public EntityBase(string _dataSource, string _queryName)
        {
            source = _dataSource;
            queryName = _queryName;
            //存在数据源则获取成员列表
            if (source.Trim().Length > 0)
            {
                memberList = new EntityManager().GetMemberList(source);
            }
        }

        /// <summary>
        /// 数据实例(其他数据库连接)
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="_manager">数据库对象</param>
        public EntityBase(string _dataSource, EntityManager _manager)
        {
            source = _dataSource;
            //存在数据源则获取成员列表
            if (source.Trim().Length > 0)
            {
                memberList = _manager.GetMemberList(source);
            }
        }

        /// <summary>
        /// 数据实例(其他数据库连接)
        /// </summary> 
        /// <param name="dataSource">数据源</param>
        /// <param name="queryName">查询名称</param>
        /// <param name="_manager">数据库对象</param>
        public EntityBase(string _dataSource, string _queryName, EntityManager _manager)
        {
            source = _dataSource;
            queryName = _queryName;
            //存在数据源则获取成员列表
            if (source.Trim().Length > 0)
            {
                memberList = _manager.GetMemberList(source);
            }
        }

        /// <summary>
        /// get;set
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return GetItem(key); }
            set { SetItem(key, value); }     
        }

        /// <summary>
        /// 以String获取结果
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetStringField(string key)
        {
            try
            {
                object data = GetItem(key);
                return data.ToString();
            }
            catch
            {
                return string.Empty;
            } 
        }

        /// <summary>
        /// 以Int获取结果
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetIntField(string key)
        {
            try
            {
                object data = GetItem(key);
                return Convert.ToInt32(data);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 以Double获取结果
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public double GetDoubleField(string key)
        {
            try
            {
                object data = GetItem(key);
                return Convert.ToDouble(data);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 以Decimal获取结果
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public decimal GetDecimalField(string key)
        {
            try
            {
                object data = GetItem(key);
                return Convert.ToDecimal(data);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 以Bool获取结果
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetBooleanField(string key)
        {
            try
            {
                object data = GetItem(key);
                return Convert.ToBoolean(data);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 以DateTime获取结果
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DateTime GetDateTimeField(string key)
        {
            try
            {
                object data = GetItem(key);
                return Convert.ToDateTime(data);
            }
            catch
            {
                return DateTime .Now;
            }
        }
 
        /// <summary>
        /// 获取成员列表
        /// </summary>
        public List<string> Members
        {
            get { return memberList; }
        }

        /// <summary>
        /// set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetItem(string key,object value)
        {
            try
            {
                if (!memberList.Equals(null) && !memberList.Contains(key)){ throw new Exception("表中无该字段!"); }

                if (data.ContainsKey(key))
                {
                    data[key] = value; 
                }
                else
                {
                    data.Add(key, value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private object GetItem(string key)
        {
            try
            {
                if (!memberList.Equals(null) && !memberList.Contains(key)) { throw new Exception("表中无该字段!"); }

                if (data.ContainsKey(key))
                {
                    return data[key];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 清楚数据
        /// </summary>
        public void SetNothing()
        {
            try
            {
                data = new Dictionary<string, object>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 已操作成员列表
        /// </summary>
        /// <returns></returns>
        public List<string> Keys
        {
            get { return data.Keys.ToList(); }
        }

        /// <summary>
        /// 获取查询名称
        /// </summary>
        /// <returns></returns>
        public string GetQueryName()
        {
            try
            {
                if (queryName.Trim().Length > 0)
                {
                    return "AS " + queryName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
