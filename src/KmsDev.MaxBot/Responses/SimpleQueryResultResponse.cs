using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Responses
{
    /// <summary>
    /// Ответ SuccessResponse схемы: success обязателен, message объясняет неуспех.
    /// </summary>
    public class SimpleQueryResultResponse : IMaxBotJsonResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; init; }

        [JsonPropertyName("message")]
        public string? Message { get; init; }
    }
}
