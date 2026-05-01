using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KmsDev.MaxBot.Handlers
{
    public static class MaxBotMessageHandlersExtension
    {
        public static IServiceCollection AddMaxBotMessageHandlerSystem(this IServiceCollection serviceCollections, Action<MaxBotMessageHandlerSystemConfig> configAction)
        {
            serviceCollections.AddSingleton<IMaxBotMessageHandlerRunner, MaxBotMessageHandlerRunnerInternal>();

            var cfg = new MaxBotMessageHandlerSystemConfig(serviceCollections);

            configAction(cfg);

            if (cfg.CustomUserStatePersistence != null)
            {
                serviceCollections.AddScoped(typeof(IMaxBotMessageHandlerUserSatePersistence), cfg.CustomUserStatePersistence);
            }
            else if (!string.IsNullOrWhiteSpace(cfg.DefaultSqliteFileName))
            {

            }
            else
            {
                serviceCollections.AddSingleton<IMaxBotMessageHandlerUserSatePersistence, MaxBotMessageHandlerInMemoryUserStatePersistenceInternal>();
            }

            return serviceCollections;
        }

        public class MaxBotMessageHandlerSystemConfig
        {
            private readonly IServiceCollection _serviceCollections;

            internal Type? CustomUserStatePersistence = null;
            internal string? DefaultSqliteFileName = null;

            public MaxBotMessageHandlerSystemConfig(IServiceCollection serviceCollections)
            {
                _serviceCollections = serviceCollections;
            }

            private void ResetPersistenceFlags()
            {
                DefaultSqliteFileName = null;
                CustomUserStatePersistence = null;
            }

            //public MaxBotMessageHandlerSystemConfig UseSqliteUserStatePersistence(string fileName)
            //{
            //    ResetPersistenceFlags();
            //    DefaultSqliteFileName = fileName;
            //    return this;
            //}

            public MaxBotMessageHandlerSystemConfig UseCustomUserStatePersistence<TPersistence>()
                where TPersistence : class, IMaxBotMessageHandlerUserSatePersistence
            {
                ResetPersistenceFlags();
                CustomUserStatePersistence = typeof(TPersistence);
                return this;
            }

            public MaxBotMessageHandlerSystemConfig AddHandler<TUserState, TAuth>(string handlerName, MaxBotMessageHandlersRouteBuilder<TUserState, TAuth> handlersBuilder)
                where TUserState : class, IMaxBotMessageHandlerUserState, new()
                where TAuth : IMaxBotMessageHandlerAuth
            {
                _serviceCollections.TryAddScoped<IMaxBotMessageHandlerRequestAccessor<TUserState>, MaxBotMessageHandlerRequestAccessorInternal<TUserState>>();
                _serviceCollections.AddKeyedScoped<IMaxBotMessageHandlerRequestAccessor>(handlerName, (sp, key) =>
                {
                    return sp.GetRequiredService<IMaxBotMessageHandlerRequestAccessor<TUserState>>();
                });

                handlersBuilder.ConfigureServices(_serviceCollections, handlerName);

                return this;
            }
        }
    }
}
