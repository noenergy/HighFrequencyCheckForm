﻿using System;

namespace DatabaseProcessing
{
    public class TotalObjectList
    {
        /// <summary>
        /// total语句
        /// </summary>
        private string total = string.Empty;
        /// <summary>
        /// group by语句
        /// </summary>
        private string group = string.Empty;

        /// <summary>
        /// 获取Total语句
        /// </summary>
        /// <returns></returns>
        public string GetTotal()
        {
            return total;
        }
        /// <summary>
        /// 获取Group语句
        /// </summary>
        /// <returns></returns>
        public string GetGroup()
        {
            return group;
        }
        /// <summary>
        /// 增加total条件
        /// </summary>
        /// <param name="totalObject">total条件</param>
        public void add(TotalObject totalObject)
        {
            try
            {
                if (total.Length == 0)
                {
                    total = totalObject.GetSelect();
                }
                else
                {
                    total += "," + totalObject.GetSelect();
                }

                if (totalObject.GetGroupby().Length > 0)
                {
                    if (group.Length == 0)
                    {
                        group = totalObject.GetGroupby();
                    }
                    else
                    {
                        string[] groupArr = totalObject.GetGroupby().Split(',');
                        foreach (string filed in groupArr)
                        {
                            if (!group.Contains(filed) && filed.Trim().Length > 0)
                            {
                                group += "," + filed;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 增加total条件
        /// </summary>
        /// <param name="key">total字段</param>
        /// <param name="totalObjectType">total条件类型</param>
        /// <param name="value">显示名称</param>
        public void add(string key, TotalObjectType totalObjectType, string value ,string groupFields)
        {
            try
            {
                add(new TotalObject(key, totalObjectType, value, groupFields));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
