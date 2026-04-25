using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateUserRemoved : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.UserRemoved;

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
        /// Пользователь, удалённый из чата
        /// </summary>
        [JsonPropertyName("user")]
        public ApiInputUser User { get; init; }

        /// <summary>
        /// Администратор, который удалил пользователя из чата. Может быть null, если пользователь покинул чат сам
        /// </summary>
        [JsonPropertyName("inviter_id")]
        public long InviterId { get; init; }

        /// <summary>
        /// Указывает, был ли пользователь удалён из канала или нет
        /// </summary>
        [JsonPropertyName("is_channel")]
        public bool IsChannel { get; init; }
    }
}
