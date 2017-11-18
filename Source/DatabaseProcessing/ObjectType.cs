namespace DatabaseProcessing
{
    /// <summary>
    /// where条件类型
    /// </summary>
    public enum WhereObjectType
    {
        /// <summary>
        /// 等于
        /// </summary>
        EqualTo = 0,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqualTo = 1,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan = 2,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqualTo = 3,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan = 4,
        /// <summary>
        /// 小于或等于
        /// </summary>
        LessThanOrEqualTo = 5,
        /// <summary>
        /// 包括
        /// </summary>
        Comprise = 6,
        /// <summary>
        /// 不包括
        /// </summary>
        NotComprise = 7,
        /// <summary>
        /// 自定义
        /// </summary>
        Extend = 8
    }
    /// <summary>
    /// total条件类型
    /// </summary>
    public enum TotalObjectType
    {
        /// <summary>
        /// 分组
        /// </summary>
        GroupBy = 0,
        /// <summary>
        /// 求和
        /// </summary>
        Sum = 1,
        /// <summary>
        /// 最大值
        /// </summary>
        Max = 2,
        /// <summary>
        /// 最小值
        /// </summary>
        Min = 3,
        /// <summary>
        /// 平均数
        /// </summary>
        Avg = 4,
        /// <summary>
        /// 计数
        /// </summary>
        Count = 5,
        /// <summary>
        /// 自定义
        /// </summary>
        Extend = 6
    }
}
