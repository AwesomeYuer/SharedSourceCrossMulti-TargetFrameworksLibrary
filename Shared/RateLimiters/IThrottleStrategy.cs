
namespace Microshaoft
{
    using System;

    /// <summary>
    /// 流量控制算法策略
    /// </summary>
    public interface IThrottleStrategy
    {
        /// <summary>
        /// 是否流控
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        bool ShouldThrottle(long n = 1);

        /// <summary>
        /// 是否流控
        /// </summary>
        /// <param name="n"></param>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        bool ShouldThrottle(long n, out TimeSpan waitTime);

        /// <summary>
        /// 是否流控
        /// </summary>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        bool ShouldThrottle(out TimeSpan waitTime);

        /// <summary>
        /// 当前令牌个数
        /// </summary>
        long CurrentTokenCount { get; }
    }
}
