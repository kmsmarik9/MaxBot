using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class GetMessagesResponse : IMaxBotJsonResponse
    {
        /// <summary>
        /// Новое созданное сообщение
        /// </summary>
        [JsonPropertyName("messages")]
        public required List<ApiInputMessage> Messages { get; init; }
    }
}
