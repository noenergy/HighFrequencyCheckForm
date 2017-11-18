using System;
using System.Collections.Generic;

namespace DatabaseProcessing
{
    public class WhereObject
    {
        /// <summary>
        /// where条件
        /// </summary>
        private string condition = string.Empty;

        /// <summary>
        /// 获取Where条件
        /// </summary>
        /// <returns></returns>
        public string GetCondition()
        {
            return condition;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">where字段</param>
        /// <param name="whereObjectType">where条件类型</param>
        /// <param name="value">值</param>
        public WhereObject(string key, WhereObjectType whereObjectType, string value)
        {
            try
            {
                if (key.Trim().Length == 0) { throw new Exception("字段名不可为空!"); }
                if (whereObjectType == WhereObjectType.Comprise || whereObjectType == WhereObjectType.NotComprise) { throw new Exception("条件类型与值不匹配!"); }
                if (whereObjectType == WhereObjectType.Extend)
                {
                    condition = string.Format("{0} {1}", key, value);
                }
                else
                {
                    condition = string.Format("{0} {1} '{2}'", key, GetType(whereObjectType), value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">where字段</param>
        /// <param name="whereObjectType">where条件类型</param>
        /// <param name="valueList">值</param>
        public WhereObject(string key, WhereObjectType whereObjectType, List<string> valueList)
        {
            try
            {
                if (key.Trim().Length == 0) { throw new Exception("字段名不可为空!"); }
                if (whereObjectType != WhereObjectType.Comprise && whereObjectType != WhereObjectType.NotComprise) { throw new Exception("条件类型与值不匹配!"); }
                if (valueList.Count == 0) { throw new Exception("查询条件列表为空!"); }

                List<string> tempList = new List<string>();
                foreach (string v in valueList)
                {
                    tempList.Add(string.Format("'{0}'", v));
                }
                string value = string.Join(",", tempList.ToArray());

                condition = string.Format("{0} {1} ({2})", key, GetType(whereObjectType), value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取where条件
        /// </summary>
        /// <param name="whereObjectType"></param>
        /// <returns></returns>
        private string GetType(WhereObjectType whereObjectType)
        {
            switch (whereObjectType)
            {
                case WhereObjectType.EqualTo :
                    return "=";
                case WhereObjectType.NotEqualTo:
                    return "<>";
                case WhereObjectType.Comprise:
                    return "IN";
                case WhereObjectType.NotComprise:
                    return "NOT IN";
                case WhereObjectType.GreaterThan:
                    return ">";
                case WhereObjectType.LessThan:
                    return "<";
                case WhereObjectType.GreaterThanOrEqualTo:
                    return ">=";
                case WhereObjectType.LessThanOrEqualTo:
                    return "<=";
                case WhereObjectType.Extend:
                    return "";
                default :
                    return "";
            }
        }
    }
}
