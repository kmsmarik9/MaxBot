using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateBotStarted
    {
        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// ID чата, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long ChatId { get; set; }

        /// <summary>
        /// Пользователь, который нажал кнопку 'Start'
        /// </summary>
        [JsonPropertyName("user")]
        public required ApiInputUser User { get; set; }

        /// <summary>
        /// Дополнительные данные из дип-линков, переданные при запуске бота
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; set; }

        /// <summary>
        /// Текущий язык пользователя в формате IETF BCP 47
        /// </summary>
        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; set; }
    }
}
