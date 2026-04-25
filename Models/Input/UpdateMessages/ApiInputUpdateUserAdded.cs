using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateUserAdded : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.UserAdded;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// ID чата, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long ChatId { get; init; }

        /// <summary>
        /// Пользователь, добавленный в чат
        /// </summary>
        [JsonPropertyName("user")]
        public ApiInputUser User { get; init; }

        /// <summary>
        /// Пользователь, который добавил пользователя в чат. Может быть null, если пользователь присоединился к чату по ссылке
        /// </summary>
        [JsonPropertyName("inviter_id")]
        public long InviterId { get; init; }

        /// <summary>
        /// Указывает, был ли пользователь добавлен в канал или нет
        /// </summary>
        [JsonPropertyName("is_channel")]
        public bool IsChannel { get; init; }
    }
}
