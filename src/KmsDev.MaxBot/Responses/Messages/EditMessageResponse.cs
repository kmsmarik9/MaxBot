using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    public class EditMessageResponse : IMaxBotJsonResponse
    {
        /// <summary>
        /// true, если запрос был успешным, false — в противном случае
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; init; }

        /// <summary>
        /// Объяснительное сообщение, если результат не был успешным
        /// </summary>
        [JsonPropertyName("message")]
        public string? ErrorMessage { get; init; }
    }
}
