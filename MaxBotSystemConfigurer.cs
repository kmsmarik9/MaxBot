using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot.Full
{
    public class MaxBotSystemConfigurer
    {
        private bool _addLongPollongManager;
        private bool _addWebhookManager;

        public MaxBotSystemConfigurer AddLongPollingManager()
        {
            _addWebhookManager = false;
            _addLongPollongManager = true;
            return this;
        }

        //TODO not impl
        //public MaxBotSystemConfigurer AddWebhookManager()
        //{
        //    _addLongPollongManager = false;
        //    _addWebhookManager = true;
        //    return this;
        //}

        internal void ConfigureServices(IServiceCollection serviceCollection)
        {
            if (_addLongPollongManager)
            {
                serviceCollection.AddSingleton<IMaxBotManager, MaxBotLongPollingManagerInternal>();
            }
            else if (_addWebhookManager)
            {
                serviceCollection.AddSingleton<IMaxBotManager, MaxBotWebhookManagerInternal>();
            }
        }
    }
}
