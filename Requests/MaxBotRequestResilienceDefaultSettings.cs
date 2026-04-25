namespace KmsDev.MaxBot.Full.Requests
{
    public class MaxBotRequestResilienceDefaultSettings : IMaxBotRequestResilienceSettings
    {
        /// <summary>
        /// default true
        /// </summary>
        public bool EnableRetryStrategy { get; set; } = true;

        /// <summary>
        /// default 3
        /// </summary>
        public int RetryMaxAttempts { get; set; } = 3;

        /// <summary>
        /// default 3 sec
        /// </summary>
        public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(3);

        /// <summary>
        /// default 30 sec
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
