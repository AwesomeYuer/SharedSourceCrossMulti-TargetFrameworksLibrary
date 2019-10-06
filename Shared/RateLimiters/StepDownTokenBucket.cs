
namespace Microshaoft
{
    /// <summary>
    /// 漏桶(满桶)
    /// </summary>
    /// <remarks>
    /// StepDownLeakyTokenBucketStrategy resembles a bucket which has been filled with tokens at the beginning but subsequently leaks tokens at a fixed interval
    /// </remarks>
    class StepDownTokenBucket : LeakyTokenBucket
    {
        public StepDownTokenBucket(long maxTokens, long refillInterval, int refillIntervalInMilliSeconds, long stepTokens, long stepInterval, int stepIntervalInMilliseconds) : base(maxTokens, refillInterval, refillIntervalInMilliSeconds, stepTokens, stepInterval, stepIntervalInMilliseconds)
        {
        }

        protected override void UpdateTokens()
        {
            var currentTime = SystemTime.UtcNow.Ticks;

            if (currentTime >= nextRefillTime)
            {
                //set tokens to max
                tokens = bucketTokenCapacity;

                //compute next refill time
                nextRefillTime = currentTime + ticksRefillInterval;
                return;
            }

            //calculate max tokens possible till the end
            var timeToNextRefill = nextRefillTime - currentTime;
            var stepsToNextRefill = timeToNextRefill/ticksStepInterval;

            var maxPossibleTokens = stepsToNextRefill*stepTokens;

            if ((timeToNextRefill % ticksStepInterval) > 0)
            {
                maxPossibleTokens += stepTokens;
            }

            if (maxPossibleTokens < tokens)
            {
                tokens = maxPossibleTokens;
            }
        }
    }
}