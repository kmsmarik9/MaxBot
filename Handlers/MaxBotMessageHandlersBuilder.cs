using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot.Full.Handlers
{
    /// <summary>
    /// Handlers builder with default auth (allow all) 
    /// </summary>
    /// <typeparam name="TUserState"></typeparam>
    public class MaxBotMessageHandlersRouteBuilder<TUserState> : MaxBotMessageHandlersRouteBuilder<TUserState, MaxBotMessageHandlersDefaultAllowAllAuth>
        where TUserState : class, IMaxBotMessageHandlerUserState, new()
    {

    }

    public class MaxBotMessageHandlersRouteBuilder<TUserState, TRootAuth>
        where TUserState: class, IMaxBotMessageHandlerUserState, new()
        where TRootAuth: IMaxBotMessageHandlerAuth
    {
        private readonly List<HandlerRouteResolveContainer> _settings = [];

        public MaxBotMessageHandlersRouteBuilder<TUserState, TRootAuth> AddCommandRoute<TMessageHandler>(string commandName, Action<MaxBotMessageHandlersRouteBuilder<TUserState, TRootAuth>>? childAction = null)
            where TMessageHandler : IMaxBotMessageHandler<TUserState>
        {
            return AddCommandRoute<TMessageHandler, TRootAuth>(commandName, childAction);
        }

        public MaxBotMessageHandlersRouteBuilder<TUserState, TRootAuth> AddCommandRoute<TMessageHandler, TRouteAuth>(string commandName, Action<MaxBotMessageHandlersRouteBuilder<TUserState, TRouteAuth>>? childAction = null)
           where TMessageHandler : IMaxBotMessageHandler<TUserState>
           where TRouteAuth : IMaxBotMessageHandlerAuth
        {
            return AddRoute<TMessageHandler, TRouteAuth>
            (
                commandName,
                requestData =>
                {
                    var messageText = requestData.UpdateMessageContainer.MessageCreated?.Message.Body?.Text ?? string.Empty;

                    if (!messageText.StartsWith('/'))
                    {
                        return false;
                    }

                    var messageCommandText = messageText
                        .Split(' ')[0]
                        .Trim()
                        .Substring(1);

                    return messageCommandText.Equals(commandName, StringComparison.OrdinalIgnoreCase);
                },
                childAction
            );
        }

        #region base
        public MaxBotMessageHandlersRouteBuilder<TUserState, TRootAuth> AddRoute<TMessageHandler>(string route, Func<MaxBotMessageHandlerRequestData, bool> predicate, Action<MaxBotMessageHandlersRouteBuilder<TUserState, TRootAuth>>? childAction = null)
           where TMessageHandler : IMaxBotMessageHandler<TUserState>
        {
            return AddRoute<TMessageHandler, TRootAuth>(route, predicate, childAction);
        }

        public MaxBotMessageHandlersRouteBuilder<TUserState, TRootAuth> AddRoute<TMessageHandler, TRouteAuth>(string route, Func<MaxBotMessageHandlerRequestData, bool> predicate, Action<MaxBotMessageHandlersRouteBuilder<TUserState, TRouteAuth>>? childAction = null)
           where TMessageHandler : IMaxBotMessageHandler<TUserState>
           where TRouteAuth: IMaxBotMessageHandlerAuth
        {

            var childBuilder = new MaxBotMessageHandlersRouteBuilder<TUserState, TRouteAuth>();

            _settings.Add(new HandlerRouteResolveContainer
            {
                Route = route,
                Predicate = predicate,
                HandlerType = typeof(TMessageHandler),
                AuthType = typeof(TRouteAuth),
                ConfigureChildServices = (services, routePrefix) =>
                {
                    childBuilder.ConfigureServices(services, routePrefix);
                }
            });

            childAction?.Invoke(childBuilder);

            return this;
        }
        #endregion

        public void ConfigureServices(IServiceCollection serviceCollection, string route)
        {
            {
                var resolveContainerItems = new List<(Func<MaxBotMessageHandlerRequestData, bool> Predicate, (string ServiceKey, string Route) Result)>();

                foreach (var settingsItem in _settings)
                {
                    var serviceKey = $"{nameof(IMaxBotMessageHandler)}_{route}#{settingsItem.Route}".ToLowerInvariant();

                    serviceCollection.AddKeyedScoped(typeof(IMaxBotMessageHandler), serviceKey, settingsItem.HandlerType);
                    serviceCollection.AddKeyedScoped(typeof(IMaxBotMessageHandlerAuth), serviceKey, settingsItem.AuthType);

                    resolveContainerItems.Add((settingsItem.Predicate, (serviceKey, settingsItem.Route)));

                    settingsItem.ConfigureChildServices(serviceCollection, $"{route}#{settingsItem.Route}");
                }

                if (resolveContainerItems.Count > 0)
                {
                    serviceCollection.AddKeyedSingleton(MaxBotMessageHandlersResolveContainerInternal.GenerateContainerName(route), (sp, containerKey) =>
                    {
                        return new MaxBotMessageHandlersResolveContainerInternal(resolveContainerItems);
                    });
                }
            }

            //#region default
            //if (_defaultMessageHandlerSettings != null)
            //{
            //    var serviceKey = $"TelegramBotDefaultHandler_{parentPrefix}";

            //    serviceCollection.AddKeyedScoped(typeof(ISweet2TelegramMessageHandlerAuthService), serviceKey, _defaultMessageHandlerSettings.AuthServiceType);
            //    serviceCollection.AddKeyedScoped(typeof(ISweet2TelegramMessageHandler), serviceKey, _defaultMessageHandlerSettings.HandlerType);

            //    _defaultMessageHandlerSettings.ChildBuilderAction(serviceCollection, $"{parentPrefix}");
            //}
            //#endregion
        }

        internal class HandlerRouteResolveContainer
        {
            public required string Route { get; init; }
            public required Func<MaxBotMessageHandlerRequestData, bool> Predicate { get; init; }
            public required Type HandlerType { get; init; }
            public required Type AuthType { get; init; }
            public required Action<IServiceCollection, string> ConfigureChildServices { get; init; }
        }
    }
}
