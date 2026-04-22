using KmsDev.MaxBot.Full.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Responses
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
