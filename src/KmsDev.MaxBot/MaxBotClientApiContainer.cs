namespace KmsDev.MaxBot
{
    public sealed class MaxBotClientApiContainer
    {
        internal MaxBotClientApiContainer(IMaxBotClient maxBotClient)
        {
            Bots = new(maxBotClient);
            Messages = new(maxBotClient);
            Subscriptions = new(maxBotClient);
            Upload = new(maxBotClient);
        }

        public BotsSection Bots { get; }
        public MessagesSection Messages { get; }
        public SubscriptionsSection Subscriptions { get; }
        public UploadSection Upload { get; }

        #region sections
        public abstract class SectionBase
        {
            internal IMaxBotClient MaxBotClient { get; }
            internal SectionBase(IMaxBotClient maxBotClient)
            {
                MaxBotClient = maxBotClient;
            }
        }

        public sealed class BotsSection : SectionBase
        {
            internal BotsSection(IMaxBotClient maxBotClient) : base(maxBotClient)
            {
            }
        }

        public sealed class MessagesSection : SectionBase
        {
            public MessagesSection(IMaxBotClient maxBotClient) : base(maxBotClient)
            {
            }
        }

        public sealed class SubscriptionsSection : SectionBase
        {
            public SubscriptionsSection(IMaxBotClient maxBotClient) : base(maxBotClient)
            {
            }
        }

        public sealed class UploadSection : SectionBase
        {
            public UploadSection(IMaxBotClient maxBotClient) : base(maxBotClient)
            {
            }
        }
        #endregion
    }
}
