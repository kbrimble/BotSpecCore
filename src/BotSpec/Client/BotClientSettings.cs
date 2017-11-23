namespace BotSpec.Client
{
    public class BotClientSettings
    {
        public int RetryTimes { get; set; } = 10;
        public int RetryWaitTimeMilliseconds { get; set; } = 1000;
    }
}