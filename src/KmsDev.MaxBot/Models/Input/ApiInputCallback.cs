using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputCallback
    {
        /// <summary>
        /// Unix-время, когда пользователь нажал кнопку
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public required DateTimeOffset TimeStamp { get; init; }

        /// <summary>
        /// Текущий ID клавиатуры
        /// </summary>
        [JsonPropertyName("callback_id")]
        public required string CallbackId { get; init; }

        /// <summary>
        /// Токен кнопки
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; init; }

        /// <summary>
        /// Пользователь, нажавший на кнопку
        /// </summary>
        [JsonPropertyName("user")]
        public required ApiInputUser User { get; init; }
    }
}
