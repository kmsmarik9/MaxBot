using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    /// <summary>
    /// Ссылка на сообщение для ответа или пересылки
    /// </summary>
    public class ApiNewMessageLink
    {
        /// <summary>
        /// reply или forward
        /// </summary>
        [JsonPropertyName("type")]
        public MaxBotMessageLinkType Type { get; set; }

        /// <summary>
        /// ID исходного сообщения
        /// </summary>
        [JsonPropertyName("mid")]
        public string? Mid { get; set; }
    }
}
