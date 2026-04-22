namespace KmsDev.MaxBot.Full.Requests
{
    public class MaxBotRequestResilienceDefaultSettings : IMaxBotRequestResilienceSettings
    {
        /// <summary>
        /// default true
        /// </summary>
        public bool EnableRetryStrategy { get; set; } = true;

        public int RetryMaxAttempts { get; set; } = 3;
        public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(3);

        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
