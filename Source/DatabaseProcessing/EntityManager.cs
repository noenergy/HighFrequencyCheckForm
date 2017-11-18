using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace DatabaseProcessing
{
    public class EntityManager
    {
        /// <summary>
        /// 数据库连接语句
        /// </summary>
        private string SQL_CONN_STR;
        /// <summary>
        /// 数据源
        /// </summary>
        private static string DATA_SOURCE = string.Empty;
        /// <summary>
        /// 数据库
        /// </summary>
        private static string DATABASE = string.Empty;
        /// <summary>
        /// 用户名
        /// </summary>
        private static string UID = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        private static string PWD = string.Empty;

        public string LastExecuteSQL = string.Empty;
        /// <summary>
        /// 读取配置文件实例化
        /// </summary>
        public EntityManager()
        {
            try
            {
                if (string.IsNullOrEmpty(DATA_SOURCE) || string.IsNullOrEmpty(DATABASE) || string.IsNullOrEmpty(UID) || string.IsNullOrEmpty(PWD))
                { 
                    ReadLogInMsg(); 
                }                
                SQL_CONN_STR = string.Format("Data Source={0};Database={1};Uid={2};Pwd={3}", DATA_SOURCE, DATABASE, UID, PWD);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 使用特定配置实例化
        /// </summary>
        /// <param name="_dataSource"></param>
        /// <param name="_database"></param>
        /// <param name="_uid"></param>
        /// <param name="_pwd"></param>
        public EntityManager(string _dataSource, string _database, string _uid, string _pwd)
        {
            try
            {
                if (string.IsNullOrEmpty(_dataSource) || string.IsNullOrEmpty(_database) || string.IsNullOrEmpty(_uid) || string.IsNullOrEmpty(_pwd)) { throw new Exception("配置信息不完整!"); }
                SQL_CONN_STR = string.Format("Data Source={0};Database={1};Uid={2};Pwd={3}", _dataSource, _database, _uid, _pwd);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        public void ReadLogInMsg()
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\config.config");

                XmlNodeList Node = doc.SelectSingleNode("config").ChildNodes;

                for (int n = 0; n < Node.Count; n++)
                {
                    XmlNodeList cNode = Node.Item(n).ChildNodes;
                    int nodeNum = cNode.Count;

                    if (Node.Item(n).Name.Equals("Database"))
                    {
                        for (int i = 0; i < nodeNum; i++)
                        {
                            try
                            {
                                if (cNode[i].Name.Equals("DataSource")) { DATA_SOURCE = cNode[i].InnerXml; }
                                if (cNode[i].Name.Equals("Database")) { DATABASE = cNode[i].InnerXml; }
                            }
                            catch
                            {
                                throw new Exception("数据库信息配置数值错误！");
                            }
                        }
                    }

                    if (Node.Item(n).Name.Equals("LogIn"))
                    {
                        for (int i = 0; i < nodeNum; i++)
                        {
                            try
                            {
                                if (cNode[i].Name.Equals("Uid")) { UID = cNode[i].InnerXml; }
                                if (cNode[i].Name.Equals("Pwd")) { PWD = cNode[i].InnerXml; }
                            }
                            catch
                            {
                                throw new Exception("登录信息配置数值错误！");
                            }
                        } 
                    }
                }
                if (string.IsNullOrEmpty(DATA_SOURCE) || string.IsNullOrEmpty(DATABASE) || string.IsNullOrEmpty(UID) || string.IsNullOrEmpty(PWD)) { throw new Exception("配置信息不完整!"); }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public List<string> GetMemberList(string tableName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                //通过数据库查询语句限制仅仅获取列名
                SqlCommand cmd = new SqlCommand(string.Format("select * from {0} where 1=0", tableName), conn);
                cmd.CommandTimeout = 3600;
                //SqlDataReader dr = cmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<string> tableColumnNameList = new List<string>();

                foreach (DataColumn dc in dt.Columns)
                {
                    tableColumnNameList.Add(dc.ColumnName);
                }

                dt.Dispose();
                da.Dispose();
                conn.Close();
                conn.Dispose();

                return tableColumnNameList; 
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取不可为空字段列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        private List<string> GetNotAllowNullList(string tableName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                //通过数据库查询语句限制仅仅获取列名
                SqlCommand cmd = new SqlCommand(string.Format("SELECT COLUMN_NAME,IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS col inner join sysobjects tbs ON tbs.name=col.TABLE_NAME WHERE tbs.name='{0}'", tableName), conn);
                cmd.CommandTimeout = 3600;
                //SqlDataReader dr = cmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<string> notAllowNullList = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["IS_NULLABLE"].Equals("NO"))
                    {
                        notAllowNullList.Add(dr["COLUMN_NAME"].ToString());
                    }
                }

                dt.Dispose();
                da.Dispose();
                conn.Close();
                conn.Dispose();

                return notAllowNullList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取为标识ID的列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        private List<string> GetIdentityList(string tableName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                //通过数据库查询语句限制仅仅获取列名
                SqlCommand cmd = new SqlCommand(string.Format("SELECT syscolumns.name AS COLUMN_NAME FROM syscolumns INNER JOIN sysobjects ON syscolumns.id = sysobjects.id WHERE COLUMNPROPERTY(syscolumns.id, syscolumns.name, 'IsIdentity') = 1 AND sysobjects.name = '{0}'", tableName), conn);
                cmd.CommandTimeout = 3600;
                //SqlDataReader dr = cmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<string> IdentityList = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    IdentityList.Add(dr["COLUMN_NAME"].ToString());
                }

                dt.Dispose();
                da.Dispose();
                conn.Close();
                conn.Dispose();

                return IdentityList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取主键列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        private List<string> GetPrimaryKeyList(string tableName)
        {
            try
            {
                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                //通过数据库查询语句限制仅仅获取列名
                SqlCommand cmd = new SqlCommand(string.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME='{0}'", tableName), conn);
                cmd.CommandTimeout = 3600;
                //SqlDataReader dr = cmd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<string> primaryKeyList = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    primaryKeyList.Add(dr["COLUMN_NAME"].ToString());
                }

                dt.Dispose();
                da.Dispose();
                conn.Close();
                conn.Dispose();

                return primaryKeyList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="dataNum">选取数据数量,0为无限制</param>
        /// <param name="orderColumn">排序字段</param>
        /// <returns></returns>
        public DataTable GetTable(EntityBase entity, int dataNum, string orderColumn)
        {
            try
            {
                string topStr = dataNum == 0 ? "" : string.Format("TOP {0}", dataNum);
                string sqlQueryStr = string.Format("select {0} * from {1}", topStr, entity.source);
                if (orderColumn.Trim().Length > 0) { sqlQueryStr += string.Format(" order by {0}", orderColumn); }
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                da.Dispose();
                conn.Close();
                conn.Dispose();

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据表(where条件)
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="whereObjectList">where对象集合</param>
        /// <param name="dataNum">选取数据数量,0为无限制</param>
        /// <param name="orderColumn">排序字段</param>
        /// <returns></returns>
        public DataTable GetTableEx(EntityBase entity, WhereObjectList whereObjectList, int dataNum, string orderColumn)
        {
            try
            {
                string topStr = dataNum == 0 ? "" : string.Format("TOP {0}", dataNum);
                string sqlQueryStr = string.Format("select {0} * from {1} {2}", topStr, entity.source, whereObjectList.where);
                if (orderColumn.Trim().Length > 0) { sqlQueryStr += string.Format(" order by {0}", orderColumn); }
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                da.Dispose();
                conn.Close();
                conn.Dispose();

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据表(total条件)
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="totalObjectList">total条件集合</param>
        /// <param name="dataNum">选取数据数量,0为无限制</param>
        /// <param name="orderColumn">排序字段</param>
        /// <returns></returns>
        public DataTable GetTableEx(EntityBase entity,TotalObjectList totalObjectList, int dataNum, string orderColumn)
        {
            try
            {
                if (totalObjectList.GetGroup().Length == 0) { throw new Exception("没有包含 GROUP BY 子句!"); }

                string topStr = dataNum == 0 ? "" : string.Format("TOP {0}", dataNum);
                string sqlQueryStr = string.Format("select {0} {1} from {2} {3} group by {4}", topStr, totalObjectList.GetTotal(), entity.source, entity.GetQueryName(), totalObjectList.GetGroup());
                if (orderColumn.Trim().Length > 0) { sqlQueryStr += string.Format(" order by {0}", orderColumn); }
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                da.Dispose();
                conn.Close();
                conn.Dispose();

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据表(where\total条件)
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="totalObjectList">total对象集合</param>
        /// <param name="whereObjectList">where对象集合</param>
        /// <param name="dataNum">选取数据数量,0为无限制</param>
        /// <param name="orderColumn">排序字段</param>
        /// <returns></returns>
        public DataTable GetTableEx(EntityBase entity, TotalObjectList totalObjectList, WhereObjectList whereObjectList, int dataNum, string orderColumn)
        {
            try
            {
                if (totalObjectList.GetGroup().Length == 0) { throw new Exception("没有包含 GROUP BY 子句!"); }

                string topStr = dataNum == 0 ? "" : string.Format("TOP {0}", dataNum);
                string sqlQueryStr = string.Format("select {0} {1} from {2} {3} {4} group by {5}", topStr, totalObjectList.GetTotal(), entity.source, entity.GetQueryName(), whereObjectList.where, totalObjectList.GetGroup());
                if (orderColumn.Trim().Length > 0) { sqlQueryStr += string.Format(" order by {0}", orderColumn); }
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                
                da.Dispose();
                conn.Close();
                conn.Dispose();

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <returns></returns>
        public bool GetEntity(ref EntityBase entityBase)
        {
            try
            {
                string sqlQueryStr = string.Format("select * from {0}", entityBase.source);

                List<string> primarykeymember = GetPrimaryKeyList(entityBase.source);
                
                if (primarykeymember.Count == 0) { throw new Exception("Table [" + entityBase.source + "] has no Primary Key!"); }

                bool firstCondition = true;
                string where = " where ";
                foreach (string wheremember in primarykeymember)
                {
                    if (!firstCondition) { where += " and "; }
                    if (entityBase[wheremember] == null) { throw new Exception("Primary Key [" + wheremember + "] not assign!"); }
                    where += wheremember + "='" + entityBase[wheremember] + "'";
                    firstCondition = false; 
                }
                sqlQueryStr += where;
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    List<string> memberList = entityBase.Members;
                    foreach (string member in memberList)
                    {
                        entityBase[member] = dr[member];
                    }
                    dr.Close();
                    conn.Close();
                    dr.Dispose();
                    conn.Dispose();
                    return true;
                }
                else
                {
                    dr.Close();
                    conn.Close();
                    dr.Dispose();
                    conn.Dispose();
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据(where条件)
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <param name="whereObjectList">where对象集合</param>
        /// <param name="orderColumn">排序字段</param>
        /// <returns></returns>
        public bool GetEntityEx(ref EntityBase entityBase, WhereObjectList whereObjectList, string orderColumn)
        {
            try
            {
                string sqlQueryStr = string.Format("select top 1 * from {0} {1}", entityBase.source, whereObjectList.where);

                if (orderColumn.Trim().Length > 0) { sqlQueryStr += string.Format(" order by {0}", orderColumn); }
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                SqlDataReader dr = cmd.ExecuteReader();
                               
                if (dr.Read())
                {
                    List<string> memberList = entityBase.Members;
                    foreach (string member in memberList)
                    {
                        entityBase[member] = dr[member];
                    }
                    dr.Close(); 
                    conn.Close();
                    dr.Dispose();
                    conn.Dispose();
                    return true;
                }
                else
                {
                    dr.Close();
                    conn.Close();
                    dr.Dispose();
                    conn.Dispose();
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据(total\where条件)
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <param name="totalObjectList">total对象集合</param>
        /// <param name="whereObjectList">where对象集合</param>
        /// <param name="orderColumn">排序字段</param>
        /// <returns></returns>
        public bool GetEntityEx(ref EntityBase entityBase, TotalObjectList totalObjectList, WhereObjectList whereObjectList, string orderColumn) 
        {
            try
            {
                if (totalObjectList.GetGroup().Length == 0) { throw new Exception("没有包含 GROUP BY 子句!"); }

                string sqlQueryStr = string.Format("select top 1 {0} from {1} {2} {3} group by {4}", totalObjectList.GetTotal(), entityBase.source, entityBase.GetQueryName(), whereObjectList.where, totalObjectList.GetGroup());

                if (orderColumn.Trim().Length > 0) { sqlQueryStr += string.Format(" order by {0}", orderColumn); }
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    List<string> memberList = new List<string>();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        memberList.Add(dr.GetName(i));
                    }

                    foreach (string member in memberList)
                    {
                        entityBase[member] = dr[member];
                    }
                    dr.Close();
                    conn.Close();
                    dr.Dispose();
                    conn.Dispose();
                    return true;
                }
                else
                {
                    dr.Close();
                    conn.Close();
                    dr.Dispose();
                    conn.Dispose();
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <returns>受影响行数</returns>
        public int DeleteEntity(EntityBase entityBase)
        {
            try
            {
                string sqlQueryStr = string.Format("delete from [{0}]", entityBase.source);

                List<string> primarykeymember = GetPrimaryKeyList(entityBase.source);

                if (primarykeymember.Count == 0) { throw new Exception("Table [" + entityBase.source + "] has no Primary Key!"); }

                bool firstCondition = true;
                string where = " where ";
                foreach (string wheremember in primarykeymember)
                {
                    if (!firstCondition) { where += " and "; }
                    if (entityBase[wheremember] == null) { throw new Exception("Primary Key [" + wheremember + "] not assign!"); }
                    where += wheremember + "='" + entityBase[wheremember] + "'";
                    firstCondition = false;
                }
                sqlQueryStr += where;
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                int num = cmd.ExecuteNonQuery();  //执行,并返回受影响行数

                conn.Close();
                conn.Dispose();
                return num;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除数据(where条件)
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <param name="whereObjectList">where条件集合</param>
        /// <returns>受影响行数</returns>
        public int DeleteEntityEx(EntityBase entityBase, WhereObjectList whereObjectList)
        {
            try
            {
                string sqlQueryStr = string.Format("delete from [{0}] {1}", entityBase.source, whereObjectList.where);
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                int num = cmd.ExecuteNonQuery();  //执行,并返回受影响行数

                conn.Close();
                conn.Dispose();
                return num;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <returns>受影响行数</returns>
        public void AddNewEntity(ref EntityBase entityBase)
        {
            try
            {
                string sqlQueryStr = string.Format("insert into [{0}]", entityBase.source);
                string sqlGetIndentityStr = string.Empty;

                List<string> notallownullmember = GetNotAllowNullList(entityBase.source);
                List<string> identityList = GetIdentityList(entityBase.source);
                List<string> memberlist = entityBase.Members;
                if (identityList.Count > 0) { sqlGetIndentityStr = ";select @@Identity"; }

                string cols = "";
                string values = "";
                List<string> collist = new List<string>();
                List<string> valuelist = new List<string>();
                
                foreach (string member in memberlist)
                {
                    object value = entityBase[member];
                    if (notallownullmember.Contains(member) && value == null) 
                    {
                        if (!identityList.Contains(member)) { throw new Exception("Key [" + member + "] is Not Allow Null!"); }
                    }
                    if (value != null)
                    {
                        collist.Add(member);
                        valuelist.Add(string.Format("'{0}'", value.ToString()));
                    }
                }

                cols = string.Format("({0})", string.Join(",", collist.ToArray()));
                values = string.Format("({0})", string.Join(",", valuelist.ToArray()));

                sqlQueryStr = string.Format("{0}{1} values{2}{3}", sqlQueryStr, cols, values, sqlGetIndentityStr);
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                object indetity = cmd.ExecuteScalar(); //执行,并返回辨识值

                if (identityList.Count > 0) { entityBase[identityList[0]] = indetity; }

                conn.Close();
                conn.Dispose();

                //获取插入数据的所有值
                GetEntity(ref entityBase);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <param name="data">数据源</param>
        public void AddNewEntity(EntityBase entityBase,DataTable data)
        {
            try
            {
                List<string> memberList = GetMemberList(entityBase.source);
                List<string> notAllowNullmemberList = GetNotAllowNullList(entityBase.source);
                List<string> identityList = GetIdentityList(entityBase.source);

                List<string> dataMember = new List<string>();
                foreach (DataColumn dc in data.Columns)
                {
                    dataMember.Add(dc.ColumnName);    
                }

                //查找不可为空的列是否有值
                foreach (string notAllowNullmember in notAllowNullmemberList)
                {
                    if (!dataMember.Contains(notAllowNullmember))
                    {
                        throw new Exception("Key [" + notAllowNullmember + "] is Not Allow Null!");
                    }
                }

                //查找是否标识列有值
                foreach (string identity in identityList)
                {
                    if (dataMember.Contains(identity)) 
                    {
                        throw new Exception("Identity [" + identity + "] Not Allow Have Value!");
                    }
                }

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlBulkCopy sbc = new SqlBulkCopy(conn);
                sbc.DestinationTableName = entityBase.source;
                sbc.BatchSize = data.Rows.Count;

                foreach (string member in dataMember)
                {
                    if (memberList.Contains(member))  //只添加当前连接表中存在的字段
                    {
                        sbc.ColumnMappings.Add(member, member);    //映射字段名 DataTable列名 ,数据库 对应的列名  
                    }
                }

                sbc.WriteToServer(data);

                conn.Close();
                conn.Dispose();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <returns>受影响行数</returns>
        public int UpdateEntity(EntityBase entityBase)
        {
            try
            {
                string sqlQueryStr = string.Format("update [{0}]", entityBase.source);

                List<string> primarykeymember = GetPrimaryKeyList(entityBase.source);
                List<string> keylist = entityBase.Keys;

                if (primarykeymember.Count == 0) { throw new Exception("Table [" + entityBase.source + "] has no Primary Key!"); }

                bool firstCondition = true;
                string where = " where ";
                foreach (string primarykey in primarykeymember)
                {
                    if (!firstCondition) { where += " and "; }
                    if (entityBase[primarykey] == null) { throw new Exception("Primary Key [" + primarykey + "] not assign!"); }
                    where += primarykey + "='" + entityBase[primarykey] + "'";
                    keylist.Remove(primarykey);
                    firstCondition = false;
                }

               
                string updatestr;
                if (keylist.Count > 0) { updatestr = " set"; }
                else { throw new Exception("No data will be updated!"); }

                List<string> templist = new List<string>();
                foreach (string key in keylist)
                {
                    object value = entityBase[key];
                    if (value != null)
                    {
                        templist.Add(string.Format(" {0} = '{1}'", key, value));
                    }
                }
                updatestr += string.Join(",", templist.ToArray());

                sqlQueryStr += updatestr + where;
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                int num = cmd.ExecuteNonQuery();  //执行,并返回受影响行数

                conn.Close();
                conn.Dispose();
                return num;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新数据(where条件)
        /// </summary>
        /// <param name="entityBase">数据实体</param>
        /// <param name="whereObjectList">where条件集合</param>
        /// <returns>受影响行数</returns>
        public int UpdateEntityEx(EntityBase entityBase, WhereObjectList whereObjectList)
        {
            try
            {
                string sqlQueryStr = string.Format("update [{0}]", entityBase.source);

                List<string> keylist = entityBase.Keys;

                string updatestr;
                if (keylist.Count > 0) { updatestr = " set"; }
                else { throw new Exception("No data will be updated!"); }

                List<string> templist = new List<string>();
                foreach (string key in keylist)
                {
                    object value = entityBase[key];
                    if (value != null)
                    {
                        templist.Add(string.Format(" {0} = '{1}'", key, value));
                    }
                }
                updatestr += string.Join(",", templist.ToArray());

                sqlQueryStr += updatestr + whereObjectList .where;
                LastExecuteSQL = sqlQueryStr;

                SqlConnection conn = new SqlConnection(SQL_CONN_STR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQueryStr, conn);
                cmd.CommandTimeout = 3600;
                int num = cmd.ExecuteNonQuery();  //执行,并返回受影响行数

                conn.Close();
                conn.Dispose();
                return num;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="para">存储过程参数</param>
        /// <returns>存储过程结果</returns>
        public DataTable ExecuteProcedureReturnTable(string procedureName, Dictionary<string, object> para)
        {
            try
            {
                if (procedureName.Trim().Equals(string.Empty)) { throw new Exception("Procedure Name not input."); }
                SqlConnection conn = new SqlConnection(SQL_CONN_STR);//SQL数据库连接对象，以数据库链接字符串为参数  
                SqlCommand cmd = new SqlCommand(procedureName, conn);//SQL语句执行对象，第一个参数是要执行的语句，第二个是数据库连接对象  
                cmd.CommandTimeout = 3600;
                cmd.CommandType = CommandType.StoredProcedure;//因为要使用的是存储过程，所以设置执行类型为存储过程  
                //依次设定存储过程的参数
                foreach (string key in para.Keys)
                {
                    cmd.Parameters.Add(new SqlParameter(key, para[key]));
                }
                //cmd.Parameters.Add("@username", SqlDbType.VarChar, 10).Value = "11";               

                conn.Open();//打开数据库连接  
                //cmd.ExecuteNonQuery();//执行存储过程  
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                da.Dispose();
                conn.Close();//关闭连接
                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="para">存储过程参数</param>
        public int ExecuteProcedureReturnNone(string procedureName, Dictionary<string, object> para)
        {
            try
            {
                if (procedureName.Trim().Equals(string.Empty)) { throw new Exception("Procedure Name not input."); }
                SqlConnection conn = new SqlConnection(SQL_CONN_STR);//SQL数据库连接对象，以数据库链接字符串为参数  
                SqlCommand cmd = new SqlCommand(procedureName, conn);//SQL语句执行对象，第一个参数是要执行的语句，第二个是数据库连接对象  
                cmd.CommandTimeout = 3600;
                cmd.CommandType = CommandType.StoredProcedure;//因为要使用的是存储过程，所以设置执行类型为存储过程  
                //依次设定存储过程的参数
                foreach (string key in para.Keys)
                {
                    if (key.Contains("@"))
                    {
                        cmd.Parameters.Add(new SqlParameter(key, para[key]));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(string.Format("@{0}", key), para[key]));
                    }
                }
                //cmd.Parameters.Add("@username", SqlDbType.VarChar, 10).Value = "11";               

                conn.Open();//打开数据库连接  
                int ret = cmd.ExecuteNonQuery();//执行存储过程  
                conn.Close();//关闭连接
                return ret;
            }
            catch
            {
                throw;
            }
        }
    }
}
