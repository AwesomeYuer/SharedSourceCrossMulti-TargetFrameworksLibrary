

namespace Microshaoft
{
    using System;
    /// <summary>
    /// 流控控制接口
    /// </summary>
    public interface ITrafficController
    {
        IThrottleStrategy InnerThrottIleStrategy { get; }

        TrafficControlStrategy FlowControlStrategy { get; }

        /// <summary>
        /// 是否流控
        /// </summary>
        /// <param name="n">个数</param>
        /// <param name="waitTime">等待时间</param>
        /// <returns>true: 流控; false: 不触发流控</returns>
        bool ShouldThrottle(long n, out TimeSpan waitTime);
    }
}
