using KmsDev.MaxBot.Models;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class GetPinnedMessageResponse : IMaxBotJsonResponse
    {
        /// <summary>
        /// Закреплённое сообщение. null, если закрепления нет.
        /// </summary>
        [JsonPropertyName("message")]
        public ApiInputMessage? Message { get; init; }
    }
}
