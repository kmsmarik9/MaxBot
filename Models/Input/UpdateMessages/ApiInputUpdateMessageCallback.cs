using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateMessageCallback : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.MessageCallback;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonPropertyName("callback")]
        public required ApiInputCallback Callback { get; set; }

        /// <summary>
        /// Изначальное сообщение, содержащее встроенную клавиатуру. Может быть null, если оно было удалено к моменту, когда бот получил это обновление
        /// </summary>
        [JsonPropertyName("message")]
        public ApiInputMessage? Message { get; set; }

        /// <summary>
        /// Текущий язык пользователя в формате IETF BCP 47
        /// </summary>
        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; set; }
    }
}
