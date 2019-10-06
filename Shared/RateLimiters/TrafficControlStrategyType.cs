namespace Microshaoft
{
    /// <summary>
    /// 流控策略类型枚举
    /// </summary>
    public enum TrafficControlStrategyType
    {
        /// <summary>
        /// TPS控制策略
        /// </summary>
        TPS,

        /// <summary>
        /// 并发控制策略
        /// </summary>
        Concurrent,

        /// <summary>
        /// 总数控制策略
        /// </summary>
        Sum,

        /// <summary>
        /// 延迟控制策略
        /// </summary>
        Delay
    }
}
