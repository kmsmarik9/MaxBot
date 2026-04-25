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
            await _userSatePersistence.SaveAsync(RequestData.HandlersPrefix, RequestData.MaxUserId, (RouteContainer.FullRoutePath, JsonSerializer.Serialize(UserState)));
        }

        private static long? GetUserId(ApiInputUpdateMessagePolymorphContainer updateMessage)
        {
            if (!updateMessage.IsPresent())
            {
                return null;
            }

            return updateMessage switch
            {
                { MessageCreated: ApiInputUpdateMessageCreated mc } => mc.Message.Sender?.UserId,
                { MessageCallback: ApiInputUpdateMessageCallback mc } => mc.Callback.User.UserId,
                { MessageEdited: ApiInputUpdateMessageEdited me } => me.Message.Sender?.UserId,
                { MessageRemoved: ApiInputUpdateMessageRemoved mr} => mr.UserId,
                { BotAdded: ApiInputUpdateBotAdded ba } => ba.User.UserId,
                { BotRemoved: ApiInputUpdateBotRemoved br } => br.User.UserId,
                { DialogMuted: ApiInputUpdateDialogMuted dm } => dm.User.UserId,
                { DialogUnmuted: ApiInputUpdateDialogUnmuted du } => du.User.UserId,
                { DialogCleared: ApiInputUpdateDialogCleared dc } => dc.User.UserId,
                { DialogRemoved: ApiInputUpdateDialogRemoved dr } => dr.User.UserId,
                { UserAdded: ApiInputUpdateUserAdded ua } => ua.User.UserId,
                { UserRemoved: ApiInputUpdateUserRemoved ur } => ur.User.UserId,
                { BotStarted: ApiInputUpdateBotStarted bs } => bs.User.UserId,
                { BotStopped: ApiInputUpdateBotStoped bs } => bs.User.UserId,
                { ChatTitleChanged: ApiInputUpdateChatTitleChanged ctc } => ctc.User.UserId,
                _ => null
             };
        }
    }
}
