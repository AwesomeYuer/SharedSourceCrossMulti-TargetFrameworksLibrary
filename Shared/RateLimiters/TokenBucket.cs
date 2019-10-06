

namespace Microshaoft
{
    using System;

    /// <summary>
    /// 令牌桶
    /// </summary>
    public abstract class TokenBucket : IThrottleStrategy
    {
        protected long bucketTokenCapacity;
        private static readonly object _syncRoot = new object();
        protected readonly long ticksRefillInterval;
        protected long nextRefillTime;


        //number of tokens in the bucket
        protected long tokens;

        protected TokenBucket
                    (
                        long bucketTokenCapacity
                        , long refillInterval
                        , long refillIntervalInMilliSeconds
                    )
        {
            if (bucketTokenCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException("bucketTokenCapacity", "bucket token capacity can not be negative");
            }

            if (refillInterval < 0)
            {
                throw new ArgumentOutOfRangeException("refillInterval", "Refill interval cannot be negative");
            }

            if (refillIntervalInMilliSeconds <= 0)
            {
                throw new ArgumentOutOfRangeException("refillIntervalInMilliSeconds", "Refill interval in milliseconds cannot be negative");
            }

            this.bucketTokenCapacity = bucketTokenCapacity;
            ticksRefillInterval = TimeSpan.FromMilliseconds(refillInterval * refillIntervalInMilliSeconds).Ticks;
        }

        /// <summary>
        /// 是否流控
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool ShouldThrottle(long n = 1)
        {
            return ShouldThrottle(n, out _);
        }


        public bool ShouldThrottle(long n, out TimeSpan waitTime)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException("n", "Should be positive integer");
            }

            lock (_syncRoot)
            {
                UpdateTokens();
                if (tokens < n)
                {
                    var timeToIntervalEnd = nextRefillTime - SystemTime.UtcNow.Ticks;
                    if (timeToIntervalEnd < 0)
                    {
                        return ShouldThrottle(n, out waitTime);
                    }

                    waitTime = TimeSpan.FromTicks(timeToIntervalEnd);
                    return true;
                }
                tokens -= n;

                waitTime = TimeSpan.Zero;
                return false;
            }
        }

        /// <summary>
        /// 更新令牌
        /// </summary>
        protected abstract void UpdateTokens();

        public bool ShouldThrottle(out TimeSpan waitTime)
        {
            return ShouldThrottle(1, out waitTime);
        }

        public long CurrentTokenCount
        {
            get
            {
                lock (_syncRoot)
                {
                    UpdateTokens();
                    return tokens;
                }
            }
        }
    }
}