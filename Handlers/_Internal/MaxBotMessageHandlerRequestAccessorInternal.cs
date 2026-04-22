using KmsDev.MaxBot.Full.Contracts;
using KmsDev.MaxBot.Full.Models;
using System.Text.Json;

namespace KmsDev.MaxBot.Full.Handlers
{
    internal class MaxBotMessageHandlerRequestAccessorInternal<TUserState> : IMaxBotMessageHandlerRequestAccessor<TUserState>
        where TUserState : class, IMaxBotMessageHandlerUserState, new()
    {
        private readonly IMaxBotMessageHandlerUserSatePersistence _userSatePersistence;

        public MaxBotMessageHandlerRouteContainer RouteContainer { get; } = new();

        public MaxBotMessageHandlerRequestData RequestData { get; private set; }

        public long MaxUserId => RequestData.MaxUserId;

        public TUserState UserState { get; private set; }

        public MaxBotMessageHandlerRequestAccessorInternal(IMaxBotMessageHandlerUserSatePersistence userSatePersistence)
        {
            _userSatePersistence = userSatePersistence;
        }

        public async Task<bool> InitAsync(IMaxBotClient maxBotClient, string handlersPrefix, ApiInputUpdateMessagePolymorphContainer updateMessage)
        {
            var userId = GetUserId(updateMessage);

            if (!userId.HasValue)
            {
                return false;
            }

            RequestData = new MaxBotMessageHandlerRequestData
            {
                MaxBotClient = maxBotClient,
                HandlersPrefix = handlersPrefix,
                MaxUserId = userId.Value,
                UpdateMessageContainer = updateMessage
            };


            {
                var persistenceValue = await _userSatePersistence.LoadAsync(RequestData.HandlersPrefix, RequestData.MaxUserId);

                //var sqlUserStateEntity = await _context.UserStates.FirstOrDefaultAsync(p => p.BotHash == botHash && p.TelegramUserId == MaxUserId);

                //if (sqlUserStateEntity == null)
                //{
                //    sqlUserStateEntity = new TelegramUserState
                //    {
                //        BotHash = botHash,
                //        TelegramUserId = MaxUserId,
                //        FullRoutePath = string.Empty,
                //        Data = "{}"
                //    };

                //    _context.UserStates.Add(sqlUserStateEntity);

                //    _sqlUserStateEntity = sqlUserStateEntity;
                //}
                //else
                //{
                //    _sqlUserStateEntity = sqlUserStateEntity;
                //}

                if (persistenceValue.HasValue) 
                {
                    RouteContainer.Init(persistenceValue.Value.Route);

                    UserState = JsonSerializer.Deserialize<TUserState>(persistenceValue.Value.Data ?? "{}")!;
                }
                else
                {
                    RouteContainer.Init("");
                    UserState = new TUserState();
                }
            }

            return true;
        }

        public async Task SaveStateChangesAsync()
        {
            //_sqlUserStateEntity.StatePath = UserState.CurrentPath;
            //_sqlUserStateEntity.Data = UserState.Data != null ? JsonSerializer.Serialize(UserState.Data) : "{}";

            //await _context.SaveChangesAsync();

            await _userSatePersistence.SaveAsync(RequestData.HandlersPrefix, RequestData.MaxUserId, (RouteContainer.FullRoutePath, JsonSerializer.Serialize(UserState)));
        }

        private static long? GetUserId(ApiInputUpdateMessagePolymorphContainer updateMessage)
        {
            if (!updateMessage.IsPresent())
            {
                return null;
            }

            return updateMessage.MessageCreated?.Message?.Sender?.UserId;
        }
    }
}
